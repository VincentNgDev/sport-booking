using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public class SaveInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SaveInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            // Get user auth to check in database
            var userId = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";

            if (context != null)
            {
                var user = await context.Set<User>().Where(o => string.Equals(o.ObjectId.ToString(), userId, StringComparison.OrdinalIgnoreCase))
                    .FirstAsync();

                var auditEntries = new List<AuditTrail>();

                foreach (var entry in context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
                {
                    bool isValidId = Guid.TryParse(entry.Properties.First(p => p.Metadata.IsPrimaryKey()).CurrentValue?.ToString(), out Guid auditObjectId);
                    string tableName = entry.Entity.GetType().Name;

                    var auditTrailEntry = new AuditTrail
                    {
                        TableName = tableName,
                        ActionType = entry.State.ToString(),
                        CreatedDateTime = DateTime.UtcNow,
                        CreateUserId = user.ObjectId,
                        AuditObjectId = isValidId ? auditObjectId : null,
                    };

                    List<AuditTrailField> fields = new List<AuditTrailField>();

                    if (entry.State == EntityState.Modified)
                    {
                        var dicOriginal = entry.OriginalValues.Properties.ToDictionary(p => p.Name, p => entry.OriginalValues[p.Name]);
                        var dicNew = entry.CurrentValues.Properties.ToDictionary(p => p.Name, p => entry.CurrentValues[p.Name]);

                        foreach (var ori in dicOriginal)
                        {
                            var field = ori.Key;
                            var oriValue = ori.Value;
                            bool isNewValueFound = dicNew.TryGetValue(field, out object? newValue);
                            var auditTrailField = new AuditTrailField 
                            {
                                AuditTrailId = auditTrailEntry.ObjectId,
                                FieldName = field,
                                TableName = tableName,
                                OldValue = oriValue?.ToString() ?? string.Empty,
                                NewValue = isNewValueFound ? newValue?.ToString() : string.Empty,
                            };
                            fields.Add(auditTrailField);
                        }
                    }
                    else if (entry.State == EntityState.Added)
                    {
                        var dicNew = entry.CurrentValues.Properties.ToDictionary(p => p.Name, p => entry.CurrentValues[p.Name]);
                        foreach (var d in dicNew)
                        {
                            var field = d.Key;
                            var newValue = d.Value;
                            var auditTrailField = new AuditTrailField
                            {
                                AuditTrailId = auditTrailEntry.ObjectId,
                                FieldName = field,
                                TableName = tableName,
                                NewValue = newValue?.ToString() ?? string.Empty,
                            };
                            fields.Add(auditTrailField);
                        }
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        var dicOriginal = entry.OriginalValues.Properties.ToDictionary(p => p.Name, p => entry.OriginalValues[p.Name]);
                        foreach (var ori in dicOriginal)
                        {
                            var field = ori.Key;
                            var oriValue = ori.Value;
                            var auditTrailField = new AuditTrailField
                            {
                                AuditTrailId = auditTrailEntry.ObjectId,
                                FieldName = field,
                                TableName = tableName,
                                OldValue = oriValue?.ToString() ?? string.Empty,
                            };
                            fields.Add(auditTrailField);
                        }
                    }

                    foreach (var field in fields)
                        auditTrailEntry.AuditTrailFields.Add(field);

                    auditEntries.Add(auditTrailEntry);
                }

                if (auditEntries.Any())
                {
                    context.AddRange(auditEntries);
                }
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
