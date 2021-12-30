using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Framework.Application;
using Fsn.Application.Interfaces;
using Fsn.Domain.Account.UserAgg;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepo _userRepo;

        public UserApplication(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<OperationResult> RemoveAllRolesByUserIdAsync(TUser user)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var result = await _userRepo.RemoveAllRolesByUserAsync(user);
                if (result.Succeeded)
                {
                    return operationResult.Succeeded();
                }
                else
                {
                    return operationResult.Failed(string.Join(" | ", result.Errors.Select(c => new { errtxt = c.Code + "-" + c.Description })));
                }

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<OperationResult> AddRoles(TUser user , string[]Roles)
        {
            OperationResult operationResult = new OperationResult();

            try
            {

                var result = await _userRepo.AddToRolesAsync(user, Roles);
                if (result.Succeeded)
                {
                    return operationResult.Succeeded();
                }
                else
                {
                    return operationResult.Failed();
                }

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<OperationResult> EditUsersRoleByAccId(string accessLevelId, string[] Roles)
        {
            OperationResult operationResult = new OperationResult();
            
            try
            {
                var _User = await _userRepo.Get.Where(c => c.AccessLevelId == Guid.Parse(accessLevelId)).ToListAsync();
                foreach (var user in _User)
                {
                    //Delete membership roles
                    var RemoveResult = await RemoveAllRolesByUserIdAsync(user);
                    if (RemoveResult.isSucceeded)
                    {
                        //Add Roles
                        var AddResult = await AddRoles(user, Roles);
                        if (AddResult.isSucceeded==false)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }

                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

    }
}