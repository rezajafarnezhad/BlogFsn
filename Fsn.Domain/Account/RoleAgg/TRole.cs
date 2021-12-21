using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fsn.Domain.Account.AccessLevel;
using Microsoft.AspNetCore.Identity;

namespace Fsn.Domain.Account.RoleAgg
{
    public class TRole:IdentityRole<Guid>
    {
        public Guid? ParentId { get; set; }
        public string PageName { get; set; }
        public int  Sort { get; set; }
        public string Description { get; set; }

        public TRole TRole_Parent { get; set; }
        public ICollection<TRole> TRole_Childs { get; set; }
        public ICollection<TAccessLevelRole> TAccessLevelRoles { get; set; }

    }
}
