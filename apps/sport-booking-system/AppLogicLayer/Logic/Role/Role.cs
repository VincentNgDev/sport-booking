using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    [ExcludeColumn(nameof(ObjectName), nameof(ObjectNumber))]
    public class Role : LogicSchemaBase
    {
        public string? RoleCode { get; set; }
        public string? RoleName { get; set; }
        public ICollection<RoleFunc> RoleFunctions { get; } = new List<RoleFunc>();
        public ICollection<Position> Positions { get; } = new List<Position>();
    }
}
