﻿using TaskList.Components.Domain.Main.Entities;

namespace TaskList.Components.Domain.Main.UseCases.Contracts
{
    public interface IUserRepository
    {
        Task<bool> AnyAsync(string email);
        Task SaveAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByTokenFromRouteAsync(string token);
        Task<User?> GetUserByTokenAsync(string id);
        void UpdateUser(User user);
        Task SaveChangesAsync();
    }
}
