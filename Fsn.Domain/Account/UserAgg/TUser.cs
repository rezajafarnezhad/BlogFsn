using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Fsn.Domain.Account.AccessLevel;
using Microsoft.AspNetCore.Identity;

namespace Fsn.Domain.Account.UserAgg
{
    public class TUser :IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public Guid AccessLevelId { get; set; }
        public TAccessLevel TAccessLevel { get; set; }

    }
}
