using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    [ExcludeColumn(nameof(ObjectName), nameof(ObjectNumber))]
    public class WorkflowNode : SchemaBase
    {
        public string? ReactFlowId { get; set; }
        public string? Type { get; set; }
        public string? Label { get; set; }
        public string? DataJson { get; set; } // Custom metadata (JSON)
        public double? PositionX { get; set; }
        public double? PositionY { get; set; }
        public Guid? WorkflowId { get; set; }
        [ForeignKey(nameof(WorkflowId))]
        public Workflow? Workflow { get; set; }
    }
}
