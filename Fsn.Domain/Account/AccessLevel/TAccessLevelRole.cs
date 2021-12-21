using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fsn.Domain.Account.RoleAgg;

namespace Fsn.Domain.Account.AccessLevel
{
    public class TAccessLevelRole
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid AccessLevelId { get; set; }

        public TRole TRole { get; set; }
        public TAccessLevel TAccessLevel { get; set; }
    }
}
