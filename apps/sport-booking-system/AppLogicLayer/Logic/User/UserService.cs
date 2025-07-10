using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public interface IUserService : IServiceBase { }

    public class UserService : ServiceBase, IUserService
    {
        public UserService(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<int> Create<T>(T viewModel, CancellationToken cancellationToken = default)
        {
            User user = new User();
            try
            {
                MapperHelper.Map(viewModel, user);

                await _dbContext.Set<User>().AddAsync(user);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AppException($"Exception occurred during create {nameof(User)}", nameof(User), null, ex);
            }
        }

        public override async Task<int> Update<T>(Guid id, T viewModel, CancellationToken cancellationToken = default)
        {
            var viewModelIdProp = viewModel?.GetType().GetProperty(nameof(User.ObjectId));
            var viewModelId = viewModelIdProp?.GetValue(viewModel, null);
            if (viewModelId != null && viewModelId is Guid && (Guid)viewModelId != id)
                throw new AppException($"id ({id}) and view model id ({viewModelId}) are not match", nameof(User));

            User? existing = await _dbContext.Set<User>().FindAsync(id);
            if (existing == null)
                throw new AppException($"{nameof(User)} not found!", nameof(User));

            try
            {
                MapperHelper.Map(viewModel, existing);
                _dbContext.Set<User>().Update(existing);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AppException($"Exception occurred during update {nameof(User)}", nameof(User), null, ex);
            }
        }
    }
}
