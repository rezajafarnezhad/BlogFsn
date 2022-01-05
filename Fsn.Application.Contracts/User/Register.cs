using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Application;

namespace Fsn.Application.Contracts.User
{
    public class CreateUser
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class EditUser:CreateUser
    {
        public string  Id { get; set; }
    }

    public class UserAccessLevel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string AccessLevelId { get; set; }
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

    public class UserInfo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string AccessLevelId { get; set; }
        public string AccessLevelTile { get; set; }
        public bool IsEmailConfirmed { get; set; }

        public string Date { get; set; }
    }

    public class ListUsers : BasePaging
    {
        public List<UserInfo> UserInfos { get; set; }
        public string Filter { get; set; }
        public string Fillter2 { get; set; }

    }
}
