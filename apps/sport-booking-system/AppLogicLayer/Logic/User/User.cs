using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public partial class User : LogicSchemaBase
    {
        public string? Email { get; set; }
        public string? Image { get; set; }
    }
}
