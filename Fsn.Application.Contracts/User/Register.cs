using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsn.Application.Contracts.User
{
    public class CreateUser
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class LoginUser
    { 
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class OutGetAllUserDetails
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string AccessLevelId { get; set; }
        public string AccessLevelTile { get; set; }
        public DateTime Data { get; set; }

    }
}
