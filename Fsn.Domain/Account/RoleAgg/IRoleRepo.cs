using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Domain;
using Fsn.Application.Contracts.Role;

namespace Fsn.Domain.Account.RoleAgg
{
    public interface IRoleRepo : IRepo<TRole>
    {
        Task<List<OutListOfRoles>> GetListOfRolesAsync();
        Task<string[]> GetRolesByAccessLevelId(string accessLevelId);
    }
}