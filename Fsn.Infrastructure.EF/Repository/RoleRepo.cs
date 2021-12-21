using Framework.Infrastructure;
using Fsn.Domain.Account.RoleAgg;
using Fsn.Infrastructure.EF.Context;

namespace Fsn.Infrastructure.EF.Repository
{
    public class RoleRepo : BaseRepo<TRole>, IRoleRepo
    {
        public RoleRepo(MainContext context) : base(context)
        {
        }
    }
}