using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Application;

namespace Fsn.Application.Contracts.AccessLevel
{
    public class AccessLevels
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
    }

    public class CreateAccesslevel
    {
        public string Title { get; set; }
        public string[] Roles { get; set; }
    }

    public class EditAccesslevel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string[] Roles { get; set; }
    }

    public class ListAccessLevels : BasePaging
    {

        public List<AccessLevels> AccessLevels { get; set; }
        public string Filter { get; set; }
    }
}
