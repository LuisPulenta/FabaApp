using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FabaApp.Common.Enums;
using FabaApp.Web.Data;
using FabaApp.Web.Data.Entities;
using FabaApp.Web.Models;
using System;
using System.Threading.Tasks;

namespace FabaApp.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly DataContext _context;

        public UserHelper(
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<UserEntity> signInManager,
            DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IdentityResult> AddUserAsync(UserEntity user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }




        public async Task AddUserToRoleAsync(UserEntity user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<UserEntity> GetUserAsync(string email)
        {
            return await _context.Users

                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity> GetUserAsync(Guid userId)
        {
            return await _context.Users

                .FirstOrDefaultAsync(u => u.Id == userId.ToString());
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }




        public async Task<bool> IsUserInRoleAsync(UserEntity user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> DeleteUserAsync(string email)
        {
            var user = await GetUserAsync(email);
            if (user == null)
            {
                return true;
            }

            var response = await _userManager.DeleteAsync(user);
            return response.Succeeded;
        }
        public async Task<IdentityResult> UpdateUserAsync(UserEntity user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(UserEntity user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                false);
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<UserEntity> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        public async Task<string> GeneratePasswordResetTokenAsync(UserEntity user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(UserEntity user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }


    }
}