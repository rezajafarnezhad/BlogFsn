using System.Threading.Tasks;
using Framework.Domain;
using Microsoft.AspNetCore.Identity;

namespace Fsn.Domain.Account.UserAgg
{
    public interface IUserRepo : IRepo<TUser>
    { 
        Task<TUser> GetUserByEmail(string email);
        Task<IdentityResult> RemoveAllRolesByUserAsync(TUser user);
        Task<IdentityResult> AddToRolesAsync(TUser user, string[] Roles);
        Task<IdentityResult> CreateUser(TUser user, string password);
        Task<IdentityResult> ChangePassword(TUser user, string oldPass, string newPass);
        Task<IdentityResult> EditUser(TUser user);
        Task<string> GenerateEmailConfirmationToken(TUser user);
        Task<IdentityResult> EmailConfirmation(TUser user, string token);
        Task<SignInResult> PasswordSignIn(TUser user, string password, bool isPersistent, bool lockoutOnFailure);
    }
}