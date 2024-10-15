using Microsoft.EntityFrameworkCore;
using TaskList.Components.Domain.Infra.Data;
using TaskList.Components.Domain.Main.Entities;

namespace TaskList.Components.Domain.Main.UseCases.Contracts
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AnyAsync(string email)
        {
            return await _context.Users.AnyAsync(x=>x.Email.Address == email);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email.Address == email);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task<User> GetUserByTokenFromRouteAsync(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task<User> GetUserByTokenAsync(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == token);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task SaveAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
