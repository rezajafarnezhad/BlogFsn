using Fsn.Domain.Account.RoleAgg;

namespace Fsn.Application.Contracts.Role
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepo _roleRepo;

        public RoleApplication(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }
    }
}