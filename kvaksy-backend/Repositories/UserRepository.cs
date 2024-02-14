using kvaksy_backend.Data;
using kvaksy_backend.Data.Models;

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
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}
