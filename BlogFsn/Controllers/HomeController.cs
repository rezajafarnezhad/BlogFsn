using BlogFsn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using BlogFsn.Common.ExMethods;
using BlogFsn.Common.MessageBox;
using Fsn.Application.Contracts.Article;
using Fsn.Application.Contracts.Commnet;
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
        private readonly ICommentApplication _commentApplication;
        private readonly ImsgBox _msgBox;
        private readonly MainContext _mainContext;

        public HomeController(IRoleRepo roleRepo, IUserRepo userRepo, IAccessLevelRepo accessLevelRepo, MainContext mainContext, IArticleApplication articleApplication, ImsgBox msgBox, ICommentApplication commentApplication)
        {
            _roleRepo = roleRepo;
            _userRepo = userRepo;
            _accessLevelRepo = accessLevelRepo;
            _mainContext = mainContext;
            _articleApplication = articleApplication;
            _msgBox = msgBox;
            _commentApplication = commentApplication;
        }

        public async Task<IActionResult> Index(int pageId = 1, int take = 9, string filter = "")
        {
            var _article = await _articleApplication.GetAllInIndexPage(pageId, take, filter);
            return View(_article);
        }
        [HttpPost]
        public async Task<IActionResult> Search(int pageId, int take, string filter)
        {

            var _article = await _articleApplication.GetAllInIndexPage(pageId, take = 9, filter);
            return View("_ArticleHomePage", _article);
        }


        [HttpGet("/{Title}/Archive")]
        public async Task<IActionResult> ArchiveCategory(string Title, int pageId = 1, int take = 9)
        {

            var _ArchiveCategory = await _articleApplication.GetArticleByCategoryTitle(pageId, take, Title);
            return View(_ArchiveCategory);
        }

        [HttpPost()]
        public async Task<IActionResult> SearchArchiveCategory(string Title, int pageId, int take)
        {

            var _ArchiveCategory = await _articleApplication.GetArticleByCategoryTitle(pageId, take = 9, Title);
            return View("_ArticleArchive", _ArchiveCategory);
        }


        [HttpGet("/{Title}/Page/{Id}")]
        public async Task<IActionResult> Article(string Id)
        {
            var _Article = await _articleApplication.GetForSinglePage(Id);
            if (_Article is null)
                return NotFound();

            ViewBag.Userid = User.GetUserDetails().UserId;

            return View(_Article);
        }


        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommnet createCommnet)
        {
            if (string.IsNullOrWhiteSpace(createCommnet.Message))
                return _msgBox.FaildMsg("کامنت خود را وارد کنید");

            var result = await _commentApplication.CreateCommnet(createCommnet);
            
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message);

            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }


        [HttpGet("/AddData")]
        public IActionResult AddData()
        {
            new AddData_Main(_roleRepo, _accessLevelRepo, _userRepo, _mainContext).Run();
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
