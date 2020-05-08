using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FabaApp.Web.Data.Entities;
using FabaApp.Web.Models;
using System;

namespace FabaApp.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserAsync(string email);
        Task<UserEntity> GetUserAsync(Guid userId);


        Task<IdentityResult> AddUserAsync(UserEntity user, string password);



        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<bool> DeleteUserAsync(string email);
        Task<IdentityResult> UpdateUserAsync(UserEntity user);
        Task<SignInResult> ValidatePasswordAsync(UserEntity user, string password);
        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);
        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user);
        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);
        Task<UserEntity> GetUserByIdAsync(string userId);
        Task<UserEntity> GetUserByEmailAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(UserEntity user);
        Task<IdentityResult> ResetPasswordAsync(UserEntity user, string token, string password);


    }
}