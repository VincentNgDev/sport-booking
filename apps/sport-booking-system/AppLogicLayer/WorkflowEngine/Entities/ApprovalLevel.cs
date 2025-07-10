using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public class ApprovalLevel
    {
        public Guid? ApprovalLevelId { get; set; }
        [ForeignKey(nameof(ApprovalLevelId))]
        public ApprovalProcess? ApprovalProcess { get; set; }
        public int Level { get; set; }
    }
}
