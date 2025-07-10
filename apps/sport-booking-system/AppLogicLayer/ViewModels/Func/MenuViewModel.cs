using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public class MenuViewModel
    {
        public string? MainUrl { get; set; }
        public List<MenuFuncViewModel> MenuFunctions { get; set; } = new List<MenuFuncViewModel>();
    }
}
