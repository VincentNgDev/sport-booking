using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public abstract class WorkflowSchemaBase : LogicSchemaBase
    {
        public Guid? WorkflowInstanceId { get; set; }
        [ForeignKey(nameof(WorkflowInstanceId))]
        public WorkflowInstance? WorkflowInstance { get; set; }
    }
}
