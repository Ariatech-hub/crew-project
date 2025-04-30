using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Business.DTOs;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Models;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services
{
    public interface IAuthService
    {
        public Task<bool> Login(string username, string password);
        public Task<ApplicationUser> GetUserByUsername(string username);
        public Task<string> GetRoleByUser(ApplicationUser user);
        public Task<ApplicationUser> Register(string username, string password, string confirmPassword, string role);
        public Task Logout();
        Task<bool> UpdateUser(ApplicationUser user);

        Task<ApplicationUser> GetUserById(string userId);

        Task ResetPassword(PasswordResetVm passwordResetVm);

        Task UnRegisterUser(UnRegisterVm unRegisterVm);

        Task ChangePassword(ChangePasswordVm changePasswordVm);

        IQueryable<ApplicationUser> GetAllUsers();

        IQueryable<ApplicationUserRole> GetAllRoles();

        Task<IEnumerable<ApplicationUserWithRole>> GetAllUsersWithRole();

        Task<string> CreateRole(string roleName);

    }

    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IGeneralUtility _generalUtility;

        public AuthService(IAuthRepository authRepository, IGeneralUtility generalUtility)
        {
            _authRepository = authRepository;
            _generalUtility = generalUtility;
           
        }

        public async Task<string> GetRoleByUser(ApplicationUser user)
        {
            return await _authRepository.GetRoleByUser(user);
        }

        public async Task<ApplicationUser> GetUserByUsername(string username)
        {
            return await _authRepository.GetUserByUsername(username);
        }

        public async Task<bool> Login(string username, string password)
        {
            return await _authRepository.Login(username, password);
        }

        public async Task Logout()
        {
            await _authRepository.Logout();
        }

        public async Task<ApplicationUser> Register(string username, string password, string confirmPassword, string role)
        {
            return await _authRepository.Register(username, password, confirmPassword, role);
        }

        public async Task<bool> UpdateUser(ApplicationUser user)
        {
            return await _authRepository.UpdateUser(user);
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _authRepository.GetUserById(userId);
        }

        public async Task ResetPassword(PasswordResetVm passwordResetVm)
        {
            await _authRepository.ResetPassword(passwordResetVm.Username, passwordResetVm.Password);
        }

        public async Task UnRegisterUser(UnRegisterVm unRegisterVm)
        {
            if (string.CompareOrdinal(unRegisterVm.UserName , _generalUtility.GetLoggedInUsername()) == 0 )

            {
                throw new Exception($"Log out to unregister {unRegisterVm.UserName}");
            }

            await _authRepository.UnRegisterUser(unRegisterVm.UserName);
        }

        public async Task ChangePassword(ChangePasswordVm changePasswordVm)
        {

            await _authRepository.ChangePassword(changePasswordVm.Username, changePasswordVm.OldPassword, changePasswordVm.Password);
            throw new NotImplementedException();
        }

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return _authRepository.GetAllUsers();
        }

        public IQueryable<ApplicationUserRole> GetAllRoles()
        {
            return _authRepository.GetAllRoles();
        }

        public async Task<IEnumerable<ApplicationUserWithRole>> GetAllUsersWithRole()
        {
            var users = await _authRepository.GetUsers();

            if (users is null)
            {
                throw new Exception("User not found");
            }

            List<ApplicationUserWithRole> applicationUserWithRoles = new List<ApplicationUserWithRole>();

            foreach (var user in users)
            {
                applicationUserWithRoles.Add( new ApplicationUserWithRole()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Role = await _authRepository.GetRoleByUserId(user.Id)

                });
            }

            return applicationUserWithRoles;


        }

        public async Task<string> CreateRole(string roleName)
        {
            await _authRepository.CreateRole(roleName);
            return roleName;
        }
    }
}
