using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public class ApprovalProcess : LogicSchemaBase
    {
        ICollection<ApprovalLevel> ApprovalLevels { get; set; } = new List<ApprovalLevel>();
    }
}
