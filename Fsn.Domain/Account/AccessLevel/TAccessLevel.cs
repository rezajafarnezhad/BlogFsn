using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Fsn.Domain.Account.UserAgg;

namespace Fsn.Domain.Account.AccessLevel
{
    public class TAccessLevel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public ICollection<TAccessLevelRole> TAccessLevelRoles { get; set; }
        public ICollection<TUser> TUsers { get; set; }
    }
}
