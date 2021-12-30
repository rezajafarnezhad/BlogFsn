using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Fsn.Application.Contracts.Role;
using Fsn.Domain.Account.RoleAgg;
using Fsn.Domain.Account.UserAgg;
using Fsn.Infrastructure.EF.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Infrastructure.EF.Repository
{
    public class RoleRepo : BaseRepo<TRole>, IRoleRepo
    {
        private readonly RoleManager<TRole> _roleManager;
        public RoleRepo(MainContext context, RoleManager<TRole> roleManager) : base(context)
        {
            _roleManager = roleManager;
        }

        public async Task<List<OutListOfRoles>> GetListOfRolesAsync()
        {
            var qData = await _roleManager.Roles
                .Select(a => new OutListOfRoles
                {
                    Id = a.Id.ToString(),
                    ParentId = a.ParentId.HasValue ? a.ParentId.Value.ToString() : null,
                    HasChild = a.TRole_Childs.Any(),
                    Sort = a.Sort,
                    Name = a.Name,
                    PageName = a.PageName,
                    Description = a.Description,
                })
                .ToListAsync();

            return qData;

        }

        public async Task<string[]> GetRolesByAccessLevelId(string accessLevelId)
        {
            var Roles = await _roleManager.Roles
                .Where(c => c.TAccessLevelRoles.Any(a => a.AccessLevelId == Guid.Parse(accessLevelId)))
                .OrderByDescending(c => c.Sort)
                .Select(c=>c.Name.ToString())
                .ToArrayAsync();
            return Roles;
        }

    }
}