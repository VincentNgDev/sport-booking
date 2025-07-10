using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    [ExcludeColumn(nameof(CreatedUser), nameof(UpdatedUser), nameof(UpdatedDateTime), nameof(ObjectName), nameof(ObjectNumber))]
    public class AuditTrailField : SchemaBase
    {
        public Guid? AuditTrailId { get; set; }

        [ForeignKey(nameof(AuditTrailId))]
        public AuditTrail? AuditTrail { get; set; }
        public string? FieldName { get; set; }
        public string? TableName { get; set; }
        [Column(TypeName = "VARCHAR(250)")]
        public string? OldValue { get; set; }
        [Column(TypeName = "VARCHAR(250)")]
        public string? OldReadableValue { get; set; }
        [Column(TypeName = "VARCHAR(250)")]
        public string? NewValue { get; set; }
        [Column(TypeName = "VARCHAR(250)")]
        public string? NewReadableValue { get; set; }
    }
}
