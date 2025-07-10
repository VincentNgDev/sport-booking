using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public abstract class ApprovalWorkflowSchemaBase : WorkflowSchemaBase
    {
        public bool IsApproved { get; set; }
        public bool IsSubmitApproval { get; set; }
    }
}
