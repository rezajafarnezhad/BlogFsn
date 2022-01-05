using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogFsn.Authentication;
using BlogFsn.Common.MessageBox;
using Fsn.Application.Contracts.Article;
using Fsn.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogFsn.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = Roles.CanManageArticle)]
    public class ArticleController : Controller
    {

        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly ImsgBox _msgBox;
        public ArticleController(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication, ImsgBox msgBox)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
            _msgBox = msgBox;
        }

        public async Task<IActionResult> Index(int PageId=1,int take=10,string filter="")
        {
            var Articles = await _articleApplication.GetAll(PageId, take, filter);
            return View(Articles);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int PageId, int take, string filter)
        {
            var Articles = await _articleApplication.GetAll(PageId, take, filter);
            return PartialView("_Article",Articles);
        }


        [Authorize(Roles = Roles.CanAddArticle)]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categorsies = new SelectList(await _articleCategoryApplication.GetForArticle(), "Id", "Title");

            return View();
        }


        [HttpPost]
        [Authorize(Roles = Roles.CanAddArticle)]
        public async Task<IActionResult> Create(CreateArticle article)
        {
            if (string.IsNullOrWhiteSpace(article.Content) || string.IsNullOrWhiteSpace(article.Title)
                                                           || article.ImagFile == null)
            {
                return _msgBox.FaildMsg("تمامی اطلاعات وارد شود");
            }

            var result = await _articleApplication.Create(article);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message,"GotoIndex()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = Roles.CanEditArticle)]
        public async Task<IActionResult> Edit(Guid Id)
        {
            ViewBag.Categorsies = new SelectList(await _articleCategoryApplication.GetForArticle(), "Id", "Title");

            var ArticleForEdit = await _articleApplication.GetForEdit(Id);

            return View(ArticleForEdit);

        }


        [HttpPost]
        [Authorize(Roles = Roles.CanEditArticle)]
        public async Task<IActionResult> Edit(Edit edit)
        {
            if (string.IsNullOrWhiteSpace(edit.Content) || string.IsNullOrWhiteSpace(edit.Title))
            {
                return _msgBox.FaildMsg("تمامی اطلاعات وارد شود");
            }

            var result = await _articleApplication.Edit(edit);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "GotoIndex()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }

        }

        [HttpPost]
        [Authorize(Roles = Roles.CanRemoveArticle)]

        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _articleApplication.Delete(Id);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }

        [HttpPost]
        [Authorize(Roles = Roles.CanChangeStatusArticle)]
        public async Task<IActionResult> ChangeStatus(Guid Id)
        {
            var result = await _articleApplication.ChangeStatus(Id);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }
    }
}
