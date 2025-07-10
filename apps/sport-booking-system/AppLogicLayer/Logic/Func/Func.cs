using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    [ExcludeColumn(nameof(ObjectName), nameof(ObjectNumber))]
    public class Func : LogicSchemaBase
    {
        public string? FuncCode { get; set; }
        public string? FuncName { get; set; }
        public string? MainCategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? MainUrl { get; set; }
        public string? SubUrl { get; set; }
        public string? Description { get; set; }
        public bool AlwaysHidden { get; set; }
        public bool AlwaysAllowAccess { get; set; }
        public bool RequiredAccess2FA { get; set; }
    }
}
