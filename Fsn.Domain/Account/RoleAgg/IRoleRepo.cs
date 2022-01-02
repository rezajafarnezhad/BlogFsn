using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Domain;
using Fsn.Application.Contracts.Role;
using Fsn.Domain.Account.UserAgg;

namespace Fsn.Domain.Account.RoleAgg
{
    public interface IRoleRepo : IRepo<TRole>
    {
        Task<List<OutListOfRoles>> GetListOfRolesAsync();
        Task<string[]> GetRolesByAccessLevelId(string accessLevelId);
        Task<List<string>> GetRolesBy(TUser user);
    }
}