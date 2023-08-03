using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UserManagement.Core.Data;
using UserManagement.Core.Models;

namespace UserManagement.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementContext _db;
        private IDbContextTransaction _transaction;


        public UserRepository(UserManagementContext context)
        {
            _db = context;
        }

        public async Task<Users> GetUser(Expression<Func<Users, bool>> predicate)
        {
            return await _db.Users.FirstOrDefaultAsync(predicate);
        }


        public async Task<bool> HasUser(Users user)
        {
            return await _db.Users.AnyAsync(x => x.Username == user.Username || x.Email == user.Email);
        }

        public Task UpdateUserStatus(UserActivations user)
        {
            _db.UserActivations.Update(user);

            _db.Users.Update(user.User);

            return _db.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _db.Database.BeginTransactionAsync(cancellationToken);

            return _transaction;
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<UserActivations> GetUserFromActivationCode(string code)
        {
            return await _db.UserActivations.Where(p => p.ActivationCode == code)
                .Include(p => p.User)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateUser(Users user)
        {
            _db.Users.Update(user);

            return await _db.SaveChangesAsync() == 1;
        }

        public async Task<bool> IsUserActivate(string activationCode)
        {
            return await _db.UserActivations.AnyAsync(x => x.ActivationCode == activationCode && x.UpdatedAt != null && x.ExpirationDate > DateTime.Now);
        }


        public async Task<Users> CreateUser(Users user)
        {
            await _db.Database.BeginTransactionAsync();

            await _db.Users.AddAsync(user);

            await _db.SaveChangesAsync();

            await _db.Database.CommitTransactionAsync();

            return user;
        }


        public async Task<bool> HasActivationCode(string activationCode)
        {
            return await _db.UserActivations.AnyAsync(x => x.ActivationCode == activationCode);
        }

        public Task<bool> HasExpiredCode(string activationCode)
        {
            //return await _db.UserActivations.AnyAsync(x => x.ActivationCode == activationCode);
            var currentDate = DateTime.Now;
            return _db.UserActivations.AnyAsync(x => x.ActivationCode == activationCode && x.ExpirationDate < currentDate);
        }
    }
}