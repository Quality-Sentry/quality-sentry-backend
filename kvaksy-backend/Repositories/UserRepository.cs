using kvaksy_backend.Data;
using kvaksy_backend.Data.Models;
using kvaksy_backend.helpers;
using Microsoft.EntityFrameworkCore;

namespace kvaksy_backend.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User> GetUser(string email, string password);
        Task<User> GetUser(string username);

    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<User> Create(User user)
        {
            try
            {
                var added = _dbContext.Add(user);

                if(added.State == EntityState.Added)
                {
                    _dbContext.SaveChanges();
                    return Task.FromResult(added.Entity);
                }

                throw new Exception("An error happened while creating the user in the database.");
            }
            catch
            {
                throw;
            }
        }

        public async Task<User> GetUser(string email, string password) 
        {
            try
            {
                var dbUser = await _dbContext
                    .Users
                    .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

                if (dbUser == null)
                {
                    _dbContext.SaveChanges();
                    throw new Exception("Unable to find user with given email and password in database");
                }

                return dbUser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUser(string username)
        {
            try
            {
                var dbUser = await _dbContext
                    .Users
                    .FirstOrDefaultAsync(u => u.Username == username);

                if (dbUser == null)
                {
                    throw new Exception("Unable to find user with given username in database");
                }

                return dbUser;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
