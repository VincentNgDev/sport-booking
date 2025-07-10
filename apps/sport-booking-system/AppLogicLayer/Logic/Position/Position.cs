using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public class Position : LogicSchemaBase
    {
        public Guid? RoleID { get; set; }
        [ForeignKey(nameof(RoleID))]
        public Role? Role { get; set; }
    }
}
