using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Fsn.Domain.Account.UserAgg;
using Fsn.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Infrastructure.EF.Repository
{
    public class UserRepo : BaseRepo<TUser>, IUserRepo
    {
        public UserRepo(MainContext context) : base(context)
        {
        }
    }
}
