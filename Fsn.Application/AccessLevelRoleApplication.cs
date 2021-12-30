using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Application;
using Framework.Infrastructure;
using Fsn.Application.Contracts.AccessLevel;
using Fsn.Application.Interfaces;
using Fsn.Domain.Account.AccessLevel;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Application
{
    public class AccessLevelRoleApplication:IAccessLevelRoleApplication
    {
        private readonly IAccessLevelRoleRepo _accessLevelRoleRepo;
        private readonly IRoleApplication _roleApplication;
        public AccessLevelRoleApplication(IAccessLevelRoleRepo accessLevelRoleRepo, IRoleApplication roleApplication)
        {
            _accessLevelRoleRepo = accessLevelRoleRepo;
            _roleApplication = roleApplication;
        }


        public async Task<OperationResult> RemoveByAccessLevelId(string accessLevelId)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var Qdata = await _accessLevelRoleRepo.Get.Where(c => c.AccessLevelId == Guid.Parse(accessLevelId)).ToListAsync();

                foreach (var Item in Qdata)
                {
                    await _accessLevelRoleRepo.DeleteAsync(Item);
                }

                await _accessLevelRoleRepo.SaveChangeAsync();
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();

            }

        }

        public async Task<OperationResult> AddRolesToAccessLevele(string accessLevelId, string[] Roles)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                foreach (var Item in Roles)
                {
                    await _accessLevelRoleRepo.CreateAsync(new TAccessLevelRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        AccessLevelId = Guid.Parse(accessLevelId),
                        RoleId = await _roleApplication.GetIdByNameRole(Item)
                    });
                }

                await _accessLevelRoleRepo.SaveChangeAsync();
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

    }
}
