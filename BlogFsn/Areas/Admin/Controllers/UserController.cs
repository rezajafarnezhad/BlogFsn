using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlogFsn.Authentication;
using BlogFsn.Common.ExMethods;
using BlogFsn.Common.MessageBox;
using Fsn.Application.Contracts.User;
using Fsn.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogFsn.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = Roles.CanManageUsers)]
    public class UserController : Controller
    {
        private readonly IUserApplication _userApplication;
        private readonly IAccessLevelApplication _accessLevelApplication;
        private readonly ImsgBox _msgBox;
        public UserController(IUserApplication userApplication, ImsgBox msgBox, IAccessLevelApplication accessLevelApplication)
        {
            _userApplication = userApplication;
            _msgBox = msgBox;
            _accessLevelApplication = accessLevelApplication;
        }

        public async Task<IActionResult> Index(int PageId = 1, int take = 10, string filter = "", string Fillter2 = "")
        {
            var _users = await _userApplication.GetAllUser(PageId, take, filter, Fillter2);
            return View(_users);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int PageId, int take, string filter, string Fillter2)
        {
            var _users = await _userApplication.GetAllUser(PageId, take, filter, Fillter2);
            return View("_Users", _users);
        }

        [HttpGet]
        [Authorize(Roles = Roles.CanAddUsers)]

        public IActionResult Create()
        {
            return View("_Create");
        }

        [HttpPost]
        [Authorize(Roles = Roles.CanAddUsers)]

        public async Task<IActionResult> Create(CreateUser createUser)
        {
            if (string.IsNullOrWhiteSpace(createUser.FullName) || string.IsNullOrWhiteSpace(createUser.Email) || string.IsNullOrWhiteSpace(createUser.Password) || string.IsNullOrWhiteSpace(createUser.ConfirmPassword) || createUser.Password != createUser.ConfirmPassword)
                return _msgBox.FaildMsg("اطلاعات را صحیح وارد کنید");

            var result = await _userApplication.CreateUser(createUser);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message.Replace(", ","</br>"));

            }
        }

        [HttpGet]
        [Authorize(Roles = Roles.CanEditUsers)]

        public async Task<IActionResult> Edit(string Id)
        {
            var _user = await _userApplication.GetForEdit(Id);
            if (_user is null)
                return NotFound();

            return View("_Edit" , _user);
        }

        [HttpPost]
        [Authorize(Roles = Roles.CanEditUsers)]

        public async Task<IActionResult> Edit(EditUser editUser)
        {
            if (string.IsNullOrWhiteSpace(editUser.FullName) || string.IsNullOrWhiteSpace(editUser.Email) || string.IsNullOrWhiteSpace(editUser.Password) || string.IsNullOrWhiteSpace(editUser.ConfirmPassword) || editUser.Password != editUser.ConfirmPassword)
                return _msgBox.FaildMsg("اطلاعات را صحیح وارد کنید");

            var result = await _userApplication.Edit(editUser);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message.Replace(", ","</br>"));
            }
        }

        [Authorize(Roles = Roles.CanChangeUsersStatus)]
        public async Task<IActionResult> ChangeStatus(string Id)
        {
            if (Id is null)
                return NotFound();

            var result = await _userApplication.ChangeStatus(Id, User.GetUserDetails().UserId);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }
        }

        [Authorize(Roles = Roles.CanRemoveUsers)]
        public async Task<IActionResult> Delete(string Id)
        {
            if (Id is null)
                return NotFound();

            var result = await _userApplication.Delete(Id, User.GetUserDetails().UserId);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = Roles.CanChangeUsersAccessLevel)]

        public async Task<IActionResult> ChangeAccessLevel(string id)
        {
            var UserAcc = await _userApplication.GetForChangeUserAcc(id);
            if (UserAcc is null)
                return NotFound();

            ViewBag.AccessLevels = new SelectList(await _accessLevelApplication.GetAccessLevelForUser(), "Id", "Name");
            return View("_ChangeAcc",UserAcc);
        }

        [HttpPost]
        [Authorize(Roles = Roles.CanChangeUsersAccessLevel)]
        public async Task<IActionResult> ChangeAccessLevel(UserAccessLevel userAccessLevel)
        {
            if (userAccessLevel.AccessLevelId=="0")
                return _msgBox.FaildMsg("اطلاعات وارد شود");

            var result = await _userApplication.ChangeUserAccessLevel(userAccessLevel.Id,User.GetUserDetails().UserId,userAccessLevel.AccessLevelId);
            if (result.isSucceeded)
            {
                CacheUsersToRebuildToken.Add(userAccessLevel.Id);
                return _msgBox.SuccessMsg(result.Message);
            }
            else
            {
                return _msgBox.FaildMsg(result.Message.Replace(", ","</br>"));
            }
        }
    }
}
