using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    [ExcludeColumn(nameof(ObjectName), nameof(ObjectNumber))]
    public class WorkflowEdge : SchemaBase
    {
        public string? ReactFlowId { get; set; }
        public string? SourceNodeId { get; set; }
        public string? TargetNodeId { get; set; }
        /// <summary>
        /// [Column] Label of this edge and also the trigger name to next node
        /// </summary>
        public string? Label { get; set; }
        public string? DataJson { get; set; }
        public Guid? WorkflowId { get; set; }
        [ForeignKey(nameof(WorkflowId))]
        public Workflow? Workflow { get; set; }
    }
}
