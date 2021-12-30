using System.Threading.Tasks;
using Framework.Domain;
using Microsoft.AspNetCore.Identity;

namespace Fsn.Domain.Account.UserAgg
{
    public interface IUserRepo : IRepo<TUser>
    {
        Task<IdentityResult> RemoveAllRolesByUserAsync(TUser user);
        Task<IdentityResult> AddToRolesAsync(TUser user, string[] Roles);
    }
}