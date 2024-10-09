using TaskList.Components.Domain.Main.Entities;

namespace TaskList.Components.Domain.Main.UseCases.Contracts
{
    public class UserRepository : IRepository
    {
        public Task<bool> AnyAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByPartialTokenAsync(string partialToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByTokenAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
