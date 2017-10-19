﻿using Microsoft.AspNet.Identity;
using MusicR8r.Data.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicR8r.AuthProvider.Conracts
{
    public interface IUserService
    {
        IIdentityMessageService SmsService { get; set; }

        Task<IdentityResult> CreateAsync(User user, string password);

        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);

        Task<bool> IsEmailConfirmedAsync(string userId);

        Task<User> FindByNameAsync(string userName);

        Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);

        Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId);

        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);

        Task<IdentityResult> CreateAsync(User user);

        Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber);

        Task<string> GetPhoneNumberAsync(string userId);

        Task<bool> GetTwoFactorEnabledAsync(string userId);

        Task<IList<UserLoginInfo>> GetLoginsAsync(string userId);

        Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo login);

        Task<User> FindByIdAsync(string userId);

        Task<IdentityResult> SetTwoFactorEnabledAsync(string userId, bool enabled);

        Task<IdentityResult> ChangePhoneNumberAsync(string userId, string phoneNumber, string token);

        Task<IdentityResult> SetPhoneNumberAsync(string userId, string phoneNumber);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<IdentityResult> AddPasswordAsync(string userId, string password);

        User FindById(string userId);

        IdentityResult AddToRole(string userId, string role);
    }
}