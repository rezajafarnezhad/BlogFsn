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
        private readonly RoleManager<TRole> _roleManager;
        public UserRepo(MainContext context, UserManager<TUser> userManager, RoleManager<TRole> roleManager) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
    }
}
