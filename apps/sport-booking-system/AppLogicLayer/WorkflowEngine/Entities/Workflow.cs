using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    //[ExcludeColumn(nameof(ObjectName), nameof(ObjectNumber))]
    public class Workflow : LogicSchemaBase
    {
        public string? Description { get; set; }
        public ICollection<WorkflowNode> WorkflowNodes { get; set; } = new List<WorkflowNode>();
        public ICollection<WorkflowEdge> WorkflowEdges { get; set; } = new List<WorkflowEdge>();
    }
}
