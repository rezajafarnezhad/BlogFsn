using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using BlogFsn.Authentication;
using BlogFsn.Common.MessageBox;
using Fsn.Application.Contracts.AccessLevel;
using Fsn.Application.Interfaces;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace BlogFsn.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.CanManageAccessLevel)]
    public class AccessLevelController : Controller
    {
        private readonly IAccessLevelApplication _accessLevelApplication;
        private readonly IUserApplication _userApplication;
        private readonly IRoleApplication _roleApplication;
        private readonly ImsgBox _msgBox;
        public AccessLevelController(IAccessLevelApplication accessLevelApplication, IRoleApplication roleApplication, ImsgBox msgBox, IUserApplication userApplication)
        {
            _accessLevelApplication = accessLevelApplication;
            _roleApplication = roleApplication;
            _msgBox = msgBox;
            _userApplication = userApplication;
        }


        public async Task<IActionResult> Index(int PageId = 1, int take = 10, string filter = "")
        {
            var AccessLevels = await _accessLevelApplication.GetAll(PageId, take, filter);
            return View(AccessLevels);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int PageId, int take, string filter)
        {
            var _AccessLevels = await _accessLevelApplication.GetAll(PageId, take, filter);
            return PartialView("_AccessLevel", _AccessLevels);
        }


        [Authorize(Roles = Roles.CanAddAccessLevel)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var ListRoles = new List<SelectListItem>();
            var Roles = await _roleApplication.ListOfRolesAsync();

            foreach (var item in Roles)
            {
                var group = new SelectListGroup();

                group.Name = item.PageName;

                var data = new SelectListItem(item.Description, item.Id)
                {
                    Group = group
                };

                ListRoles.Add(data);
            }

            ViewBag.ListRole = ListRoles;
            return View("_Create");
        }

        [HttpPost]
        [Authorize(Roles = Roles.CanAddAccessLevel)]

        public async Task<IActionResult> Create(CreateAccesslevel accesslevel)
        {
            if (string.IsNullOrWhiteSpace(accesslevel.Title) || accesslevel.Roles == null)
                return _msgBox.FaildMsg("وارد کردن اطلاعات الزامی می باشد");

            var result = await _accessLevelApplication.Create(accesslevel);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "GotoIndex()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }
        }


        [Authorize(Roles = Roles.CanEditAccessLevel)]
        public async Task<IActionResult> Edit(string Id)
        {
            if (Id is null)
                return _msgBox.FaildMsg("پیدا نشد");

            var AccessLevel = await _accessLevelApplication.GetForEdit(Id);
            AccessLevel.Roles = await _roleApplication.GetRolesByAccessLevelId(Id);

            var ListRoles = new List<SelectListItem>();
            var Roles = await _roleApplication.ListOfRolesAsync();

            foreach (var item in Roles)
            {
                var group = new SelectListGroup();

                group.Name = item.PageName;

                var data = new SelectListItem(item.Description, item.Name)
                {
                    Group = group
                };

                foreach (var Role in AccessLevel.Roles)
                {
                    if (item.Name == Role)
                    {
                        data.Selected = true;
                    }
                }

                ListRoles.Add(data);
            }
            ViewBag.ListRole = ListRoles;
            return View("_Edit", AccessLevel);

        }

        [Authorize(Roles = Roles.CanEditAccessLevel)]
        [HttpPost]
        public async Task<IActionResult> Edit(EditAccesslevel accesslevel)
        {

            if (string.IsNullOrWhiteSpace(accesslevel.Title) || accesslevel.Roles == null || accesslevel.Roles.Count() == 0)
                return _msgBox.FaildMsg("وارد کردن اطلاعات الزامی می باشد");

            var result = await _accessLevelApplication.Edit(accesslevel);
            if (result.isSucceeded)
            {
                //Update Access Level User
                var UpdateRolesResult = await _userApplication.EditUsersRoleByAccId(accesslevel.Id, accesslevel.Roles);
                if (UpdateRolesResult.isSucceeded)
                {
                    //Save ,{List} users ID  of users at this access level for update tokens 
                    var UserIds = await _userApplication.GetUserIdsByAcccessLevelId(accesslevel.Id);
                    CacheUsersToRebuildToken.AddRenge(UserIds);

                    return _msgBox.SuccessMsg(UpdateRolesResult.Message, "GotoIndex()");
                }
                else
                {
                    return _msgBox.FaildMsg(result.Message);
                }
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }

        }


        [HttpPost]
        [Authorize(Roles = Roles.CanRemoveAccessLevel)]

        public async Task<IActionResult> Delete(string id)
        {
            var Result = await _accessLevelApplication.Delete(id);
            if (Result.isSucceeded)
            {
                return _msgBox.SuccessMsg(Result.Message, "RefreshTbl()");
            }
            else
            {
                return _msgBox.FaildMsg(Result.Message);
            }
        }

    }
}
