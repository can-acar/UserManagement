using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;
using UserManagement.Core.Models;

namespace UserManagement.Core.Repositories
{
    public interface IUserRepository
    {
        Task<Users> GetUser(Expression<Func<Users, bool>> predicate);
        Task<bool> HasUser(Users user);
        Task<Users> CreateUser(Users user);
        Task<bool> HasActivationCode(string activationCode);
        Task<bool> HasExpiredCode(string activationCode);
        Task UpdateUserStatus(UserActivations user);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitAsync();

        Task RollbackAsync();

        Task<UserActivations> GetUserFromActivationCode(string code);
        Task<bool> UpdateUser(Users user);
        Task<bool> IsUserActivate(string activationCode);
    }
}