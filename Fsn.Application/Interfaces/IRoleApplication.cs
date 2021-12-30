using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fsn.Application.Contracts.Role;

namespace Fsn.Application.Interfaces
{
    public interface IRoleApplication
    {
        Task<List<OutListOfRoles>> ListOfRolesAsync();
        Task<string[]> GetRolesByAccessLevelId(string accessLevelId);
        Task<Guid> GetIdByNameRole(string name);
    }
}
