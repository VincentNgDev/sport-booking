using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    /// <summary>
    /// A base interface of service for dependency injection
    /// </summary>
    public interface IServiceBase
    {
        Task<IEnumerable<T>> LoadList<T>(Expression<Func<Func, bool>>? predicate = default, bool includeDeactivatedObjects = false, CancellationToken cancellationToken = default);
        Task<PaginatedViewModel<T>> LoadPaginatedList<T>(int pageNumber = 1, int pageSize = 10, Expression<Func<Func, bool>>? predicate = default, bool includeDeactivatedObjects = false, CancellationToken cancellationToken = default);
        Task<T> Load<T>(Expression<Func<Func, bool>>? predicate = default, bool includeDeactivatedObjects = false, CancellationToken cancellationToken = default);
        Task<T> Load<T>(Guid id, bool includeDeactivatedObjects = false, CancellationToken cancellationToken = default);
        Task<int> Create<T>(T viewModel, CancellationToken cancellationToken = default);
        Task<int> Update<T>(Guid id, T viewModel, CancellationToken cancellationToken = default);
        Task<bool> Delete<T>(Expression<Func<Func, bool>> predicate, CancellationToken cancellationToken = default);
        Task ExecuteInTransactionAsync(Func<Task> operation);
    }
}
