using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fsn.Application.Contracts.Role;
using Fsn.Application.Interfaces;
using Fsn.Domain.Account.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepo _roleRepo;

        public RoleApplication(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public async Task<List<OutListOfRoles>> ListOfRolesAsync()
        {
            return await _roleRepo.GetListOfRolesAsync();
        }

        public async Task<string[]> GetRolesByAccessLevelId(string accessLevelId)
        {
            try
            {
                return await _roleRepo.GetRolesByAccessLevelId(accessLevelId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Guid> GetIdByNameRole(string name)
        {
            return await _roleRepo.Get.Where(c => c.Name == name).Select(c => c.Id).SingleOrDefaultAsync();
        }

    }
}