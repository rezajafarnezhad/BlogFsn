using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Fsn.Domain.Account.RoleAgg;
using Fsn.Domain.Account.UserAgg;
using Fsn.Infrastructure.EF.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Infrastructure.EF.Repository
{
    public class UserRepo : BaseRepo<TUser> , IUserRepo
    {

        private readonly UserManager<TUser> _userManager;
        private readonly SignInManager<TUser> _signInManager;

        public UserRepo(MainContext context, UserManager<TUser> userManager, SignInManager<TUser> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<TUser> GetUserByEmail(string email)
        {
            return await Get.Where(c => c.Email == email).SingleOrDefaultAsync();
        }

        public async Task<IdentityResult> RemoveAllRolesByUserAsync(TUser user)
        {
            var qRoles = await _userManager.GetRolesAsync(user);
            return  await _userManager.RemoveFromRolesAsync(user, qRoles);
        }

        public async Task<IdentityResult> AddToRolesAsync(TUser user, string[] Roles)
        {
            return await _userManager.AddToRolesAsync(user, Roles);
        }

        public async Task<IdentityResult> CreateUser(TUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<string> GenerateEmailConfirmationToken(TUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> EmailConfirmation(TUser user, string token)
        {
           return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<SignInResult> PasswordSignIn(TUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(user,password, isPersistent, lockoutOnFailure);
        }
    }
}
