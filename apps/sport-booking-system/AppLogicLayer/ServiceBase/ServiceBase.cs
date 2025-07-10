using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public abstract class ServiceBase : IServiceBase
    {
        protected readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor? _httpContextAccessor;
        protected ServiceBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = default;
        }
        public ServiceBase(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public virtual Task<int> Create<T>(T viewModel, CancellationToken cancellationToken = default) 
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Delete<T>(Expression<Func<Func, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> Load<T>(Expression<Func<Func, bool>>? predicate = null, bool includeDeactivatedObjects = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> Load<T>(Guid id, bool includeDeactivatedObjects = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> LoadList<T>(Expression<Func<Func, bool>>? predicate = null, bool includeDeactivatedObjects = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<PaginatedViewModel<T>> LoadPaginatedList<T>(int pageNumber = 1, int pageSize = 10, Expression<Func<Func, bool>>? predicate = null, bool includeDeactivatedObjects = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> Update<T>(Guid id, T viewModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteInTransactionAsync(Func<Task> operation)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await operation();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
