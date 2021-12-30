using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsn.Application.Contracts.Role
{
    public class OutListOfRoles
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string PageName { get; set; }
        public int Sort { get; set; }
        public string Description { get; set; }
        public bool HasChild { get; set; }
    }
}
