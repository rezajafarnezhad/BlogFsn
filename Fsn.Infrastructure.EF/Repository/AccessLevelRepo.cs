using Framework.Infrastructure;
using Fsn.Domain.Account.AccessLevel;
using Fsn.Infrastructure.EF.Context;

namespace Fsn.Infrastructure.EF.Repository
{
    public class AccessLevelRepo : BaseRepo<TAccessLevel>, IAccessLevelRepo
    {
        public AccessLevelRepo(MainContext context) : base(context)
        {
        }
    }
}