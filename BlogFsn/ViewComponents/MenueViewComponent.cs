using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fsn.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogFsn.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public MenuViewComponent(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var _model =await _articleCategoryApplication.GetCategoryForMenu();
            return View(_model);
        }

    }
}
