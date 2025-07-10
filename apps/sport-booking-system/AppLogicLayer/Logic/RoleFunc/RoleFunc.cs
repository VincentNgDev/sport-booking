using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public class RoleFunc : SchemaBase
    {
        public Guid? RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }
        public Guid? FuncId { get; set; }
        [ForeignKey(nameof(FuncId))]
        public Func? Function { get; set; }
        public bool AllowCreate { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowView { get; set; }
    }
}
