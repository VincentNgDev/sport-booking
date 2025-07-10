using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public interface IFuncMenuService : IServiceBase 
    {
        Task<List<MenuViewModel>> LoadMenu();
    }
    public class FuncMenuService : ServiceBase, IFuncMenuService
    {
        public FuncMenuService(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<T>> LoadList<T>(Expression<Func<Func, bool>>? predicate = null, bool includeDeactivatedObjects = false, CancellationToken cancellationToken = default)
        {
            var query = includeDeactivatedObjects ? _dbContext.Set<Func>() : _dbContext.Set<Func>().Where(o => o.IsDeleted == true);
            if (predicate != null)
                query = query.Where(predicate);

            var data = await query.AsNoTracking().ToListAsync();

            return MapperHelper.Map<Func, T>(data);
        }

        public async Task<List<MenuViewModel>> LoadMenu()
        {
            List<MenuViewModel> menuViewModels = new List<MenuViewModel>();
            var query = _dbContext.Set<Func>().Where(o => o.IsDeleted == true);
            var funcs = await query.AsNoTracking().ToListAsync();

            foreach (var group in funcs.GroupBy(o => o.MainUrl))
            {
                var menu = new MenuViewModel();
                menu.MainUrl = group.Key;
                List<MenuFuncViewModel> menuFuncs = new List<MenuFuncViewModel>();
                foreach (var func in group)
                {
                    menuFuncs.Add(new MenuFuncViewModel
                    {
                        FuncName = func.FuncName,
                        FuncCode = func.FuncCode,
                    });
                }
                menu.MenuFunctions.AddRange(menuFuncs);
                menuViewModels.Add(menu);
            }

            return menuViewModels;
        }
    }
}
