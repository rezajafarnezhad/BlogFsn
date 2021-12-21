using Framework.Infrastructure;
using Fsn.Domain.Account.AccessLevel;
using Fsn.Infrastructure.EF.Context;

namespace Fsn.Infrastructure.EF.Repository
{
    public class AccessLevelRoleRepo : BaseRepo<TAccessLevelRole>, IAccessLevelRoleRepo
    {
        public AccessLevelRoleRepo(MainContext context) : base(context)
        {
        }
    }
}