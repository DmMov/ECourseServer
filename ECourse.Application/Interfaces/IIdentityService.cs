using Microsoft.AspNetCore.Identity;
using ECourse.Application.Models;
using ECourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECourse.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<User> GetUserAsync(string email);
        Task<User> GetUserAsync(int userId);
        Task<(Result Result, string email)> CreateUserAsync(User user, string password);
        Task<bool> ChekPasswordAsync(User user, string password);
        Task LoginAsync(User user);
        Task<bool> IsEmailConfirmedAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<string> GetUserRoleAsync(User user);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
    }
}
