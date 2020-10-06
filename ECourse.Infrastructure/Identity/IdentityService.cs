using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using ECourse.Application.Interfaces;
using ECourse.Application.Models;
using ECourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ECourse.Infrastructure.Identity
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMailSenderService mailSenderService;

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager, IMailSenderService mailSenderService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mailSenderService = mailSenderService;
        }

        public async Task<User> GetUserAsync(int userId) => 
            await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

        public async Task<User> GetUserAsync(string email) =>
            await userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper());

        public async Task<(Result Result, string email)> CreateUserAsync(User user, string password)
        {
            IdentityResult result = await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "student");

            string confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

            confirmationToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));

            string confirmationLink = $"http://localhost:8080/confirm-email/{user.Id}/{confirmationToken}";

            await mailSenderService.SendEmailAsync(user.Email,
                    "ECourse Email Confirmation!",
                    $"<h2>Hello, {user.UserName}</h2>" +
                    $"<p>Welcome to ECourse, <a href='{confirmationLink}'>Confirm</a> Your Email!</p>");

            return (result.ToApplicationResult(), user.Email);
        }

        public async Task<bool> ChekPasswordAsync(User user, string password) =>
            await userManager.CheckPasswordAsync(user, password);

        public async Task LoginAsync(User user) =>
            await signInManager.SignInAsync(user, isPersistent: false);

        public async Task<bool> IsEmailConfirmedAsync(User user) =>
            await userManager.IsEmailConfirmedAsync(user);

        public async Task<bool> UpdateUserAsync(User user) =>
            (await userManager.UpdateAsync(user)).Succeeded == true;

        public async Task<string> GetUserRoleAsync(User user) =>
            (await userManager.GetRolesAsync(user)).First();

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            User user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return IdentityResult.Failed();

            Regex regex = new Regex("^[A-Za-z0-9_-]+$");

            if (!regex.IsMatch(token))
                return IdentityResult.Failed();
            
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            return await userManager.ConfirmEmailAsync(user, token);
        }
    }
}