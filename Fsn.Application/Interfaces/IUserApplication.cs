using System.Threading.Tasks;
using Framework.Application;
using Fsn.Application.Contracts.User;
using Fsn.Domain.Account.UserAgg;

namespace Fsn.Application.Interfaces
{
    public interface IUserApplication
    {
        Task<OperationResult> RemoveAllRolesByUserIdAsync(TUser user);
        Task<OperationResult> AddRoles(TUser user , string[]Roles);
        Task<OperationResult> EditUsersRoleByAccId(string accessLevelId, string[] Roles);
        Task<string> GenerateEmailConfirmationToken(TUser user);
        Task<OperationResult> Register(CreateUser createUser);
        Task<OperationResult> EmailConfirmation(string token);
        Task<OperationResult> Login(LoginUser loginUser);
        Task<OutGetAllUserDetails> GetAllUserDetailsAsync(string userId);
        Task<TUser> GetUserById(string userid);
    }
}
