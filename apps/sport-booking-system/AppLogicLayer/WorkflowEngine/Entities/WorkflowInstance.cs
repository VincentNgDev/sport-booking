using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public class WorkflowInstance : SchemaBase
    {
        public Guid? WorkflowId { get; set; }
        [ForeignKey(nameof(WorkflowId))]
        public Workflow? Workflow { get; set; }

        public Guid? CurrentNodeId { get; set; }
        public WorkflowNode? CurrentWorkflowNode { get; set; }

        public string? Status { get; set; }

        /// <summary>
        /// [Column] Related module object type name that implemented workflow process
        /// </summary>
        public string? RelatedObjectTypeName { get; set; }
        /// <summary>
        /// [Column] Related object id that implemented this workflow process
        /// </summary>
        public Guid? RelatedObjectId { get; set; }
    }
}
