using System.Threading.Tasks;
using Framework.Application;
using Fsn.Domain.Account.UserAgg;

namespace Fsn.Application.Interfaces
{
    public interface IUserApplication
    {
        Task<OperationResult> RemoveAllRolesByUserIdAsync(TUser user);
        Task<OperationResult> AddRoles(TUser user , string[]Roles);
        Task<OperationResult> EditUsersRoleByAccId(string accessLevelId, string[] Roles);
    }
}
