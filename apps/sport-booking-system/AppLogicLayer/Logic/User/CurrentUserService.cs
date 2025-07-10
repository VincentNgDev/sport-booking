using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public interface ICurrentUserService : IServiceBase 
    {
        Guid? CurrentUserId { get; }
        string? CurrentUserName { get; }
    }
    public class CurrentUserService : ServiceBase, ICurrentUserService
    {
        public CurrentUserService(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {

        }

        private Guid? _currentUserID 
        { 
            get 
            {
                // Get user object id
                throw new NotImplementedException();
            } 
        }

        private string? _currentUserName 
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Guid? CurrentUserId => _currentUserID;

        public string? CurrentUserName => _currentUserName;
    }
}
