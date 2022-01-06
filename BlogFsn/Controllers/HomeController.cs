using BlogFsn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Fsn.Application.Interfaces;
using Fsn.Domain.Account.AccessLevel;
using Fsn.Domain.Account.RoleAgg;
using Fsn.Domain.Account.UserAgg;
using Fsn.Domain.Blog;
using Fsn.Infrastructure.EF.Context;
using Fsn.Infrastructure.EF.Data;

namespace BlogFsn.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoleRepo _roleRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAccessLevelRepo _accessLevelRepo;
        private readonly IArticleApplication _articleApplication;
        private readonly MainContext _mainContext;

        public HomeController(IRoleRepo roleRepo, IUserRepo userRepo, IAccessLevelRepo accessLevelRepo, MainContext mainContext, IArticleApplication articleApplication)
        {
            _roleRepo = roleRepo;
            _userRepo = userRepo;
            _accessLevelRepo = accessLevelRepo;
            _mainContext = mainContext;
            _articleApplication = articleApplication;
        }

        public async Task<IActionResult> Index(int pageId=1 , int take=9 , string filter="")
        {
            var _article = await _articleApplication.GetAll(pageId, take, filter);
            return View(_article);
        }
        [HttpPost]
        public async Task<IActionResult> Search(int pageId, int take, string filter)
        {

            var _article = await _articleApplication.GetAll(pageId, take=9, filter);
            return View("_ArticleHomePage",_article);
        }


        [HttpGet("/AddData")]
        public IActionResult AddData()
        {
            new AddData_Main(_roleRepo,_accessLevelRepo,_userRepo,_mainContext).Run();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
