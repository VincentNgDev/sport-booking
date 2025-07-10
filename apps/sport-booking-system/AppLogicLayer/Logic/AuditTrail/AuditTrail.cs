using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    [ExcludeColumn(nameof(UpdatedUser), nameof(UpdatedDateTime), nameof(ObjectName), nameof(ObjectNumber))]
    public class AuditTrail : SchemaBase
    {
        public Guid? AuditObjectId { get; set; }
        public Guid? CreateUserId { get; set; }
        public string? TableName { get; set; }
        public string? ActionType { get; set; }
        public ICollection<AuditTrailField> AuditTrailFields { get; } = new List<AuditTrailField>();
    }
}
    