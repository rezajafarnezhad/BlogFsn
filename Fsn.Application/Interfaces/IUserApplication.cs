using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Application;
using Fsn.Application.Contracts.User;
using Fsn.Domain.Account.UserAgg;

namespace Fsn.Application.Interfaces
{
    public interface IUserApplication
    {
        Task<OperationResult> RemoveAllRolesByUserAsync(TUser user);
        Task<OperationResult> AddRoles(TUser user , string[]Roles);
        Task<OperationResult> EditUsersRoleByAccId(string accessLevelId, string[] Roles);
        Task<string> GenerateEmailConfirmationToken(TUser user);
        Task<OperationResult> Register(CreateUser createUser);
        Task<OperationResult> EmailConfirmation(string token);
        Task<OperationResult> Login(LoginUser loginUser);
        Task<OutGetAllUserDetails> GetAllUserDetailsAsync(string userId);
        Task<TUser> GetUserById(string userid);
        Task<ListUsers> GetAllUser(int pageid, int take, string filter,string Fillter2);
        Task<OperationResult> Delete(string id, string operatorId);
        Task<OperationResult> ChangeStatus(string id, string operatorId);
        Task<OperationResult> CreateUser(CreateUser createUser);
        Task<EditUser> GetForEdit(string id);
        Task<OperationResult> Edit(EditUser editUser);
        Task<UserAccessLevel> GetForChangeUserAcc(string id);
        Task<OperationResult> ChangeUserAccessLevel(string userId, string operatorId, string accessLevelId);
        Task<List<string>> GetUserIdsByAcccessLevelId(string accessLevelId);
    }
}
