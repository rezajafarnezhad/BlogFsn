using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogFsn.Authentication;
using BlogFsn.Common.ExMethods;
using BlogFsn.Common.MessageBox;
using Fsn.Application.Contracts.Article;
using Fsn.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BlogFsn.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.CanManageCategories)]
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


        [Authorize(Roles = Roles.CanAddCategory)]

        public IActionResult Create()
        {

            return View("Create");
        }

        [HttpPost]
        [Authorize(Roles = Roles.CanAddCategory)]

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

        [Authorize(Roles = Roles.CanEditCategory)]

        public async Task<IActionResult> Edit(Guid id)
        {
            var Model = await _articleCategoryApplication.GetForEdit(id);
            return View("edit", Model);
        }


        [HttpPost]
        [Authorize(Roles = Roles.CanEditCategory)]

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
        [Authorize(Roles = Roles.CanRemoveCategory)]

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
        [Authorize(Roles = Roles.CanChangeStatusCategory)]

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
