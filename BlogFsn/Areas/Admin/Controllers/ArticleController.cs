﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fsn.Application.Contracts.Article;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;

namespace BlogFsn.Areas.Admin.Controllers
{

    [Area("Admin")]
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



        public async Task<IActionResult> Create()
        {
            ViewBag.Categorsies = new SelectList(await _articleCategoryApplication.GetForArticle(), "Id", "Title");

            return View();
        }


        [HttpPost]
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

    }
}