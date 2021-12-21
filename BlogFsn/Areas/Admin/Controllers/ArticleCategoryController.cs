using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogFsn.Common.ExMethods;
using Fsn.Application.Contracts.Article;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;

namespace BlogFsn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleCategoryController : Controller
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly ImsgBox _msgBox;
        public ArticleCategoryController(IArticleCategoryApplication articleCategoryApplication, ImsgBox msgBox)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _msgBox = msgBox;
        }

        public async Task<IActionResult> Index(int pageId = 1, int take = 10)
        {

            var AllCategory = await _articleCategoryApplication.GetAll(pageId, take);
            return View(AllCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int pageId, int take)
        {
            var AllCategory = await _articleCategoryApplication.GetAll(pageId, take);
            return PartialView("_Category", AllCategory);
        }


        public IActionResult Create()
        {

            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Title)
        {
            if (!ModelState.IsValid)
                return _msgBox.ModelStateMsg(ModelState.GetErrors());

            var CreateArticleCategory = new CreateArticleCategory() { Title = Title };
            var result = await _articleCategoryApplication.Create(CreateArticleCategory);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var Model = await _articleCategoryApplication.GetForEdit(id);
            return View("edit", Model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CategoryForEdit categoryForEdit)
        {
            if (string.IsNullOrWhiteSpace(categoryForEdit.Title))
                return _msgBox.FaildMsg("اطلاعات پر شود");

            var result = await _articleCategoryApplication.Edit(categoryForEdit);
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
        public async Task<IActionResult> Delete(Guid Id)
        {
            
            var result = await _articleCategoryApplication.Delete(Id);
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
        public async Task<IActionResult> ChangeStatus(Guid Id)
        {

            var result = await _articleCategoryApplication.ChangeStatuse(Id);
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
