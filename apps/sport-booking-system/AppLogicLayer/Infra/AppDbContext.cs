using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public class AppDbContext : DbContext
    {
        //private readonly ICurrentUserService? _currentUserService;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            //_currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Get the current assembly (or any assembly where your models live)
            var entityTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(SchemaBase).IsAssignableFrom(t));

            foreach (var type in entityTypes)
            {
                var entityBuilder = modelBuilder.Entity(type); // Dynamically register entity

                // Get the exclude column attribute of each type if exists and exclude the columns in database
                var excludeAttr = (ExcludeColumnAttribute?)Attribute.GetCustomAttribute(type, typeof(ExcludeColumnAttribute));
                if (excludeAttr != null && excludeAttr.ColumnNames.Any())
                {
                    var efEntityType = modelBuilder.Model.FindEntityType(type);
                    foreach (var columnName in excludeAttr.ColumnNames)
                    {
                        var property = efEntityType?.FindProperty(columnName);
                        if (property != null && efEntityType != null)
                        {
                            efEntityType.RemoveProperty(property);
                            entityBuilder.Ignore(columnName);
                        }
                    }
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            //ApplyAuditInfo();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ApplyAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInfo()
        {
            var userId = Guid.NewGuid();//_currentUserService?.CurrentUserId;
            var userName = "";//_currentUserService?.CurrentUserName;

            DateTime now = DateTime.UtcNow;

            var entries = ChangeTracker.Entries<SchemaBase>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDateTime = now;
                    entry.Entity.CreatedUser = userName;
                    entry.Entity.UpdatedDateTime = now;
                    entry.Entity.UpdatedUser = userName;
                    entry.Entity.IsDeleted = false;

                    var userIdProp = entry.Entity.GetType().GetProperty(nameof(LogicSchemaBase.CreatedUserId));
                    if (userIdProp != null)
                        userIdProp.SetValue(entry.Entity, userId, null);
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDateTime = now;
                    entry.Entity.UpdatedUser = userName;
                }
            }
        }
    }
}
