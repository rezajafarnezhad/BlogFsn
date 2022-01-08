using System.Threading.Tasks;
using Fsn.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogFsn.ViewComponents
{
    public class LastArticleViewComponent : ViewComponent
    {
        private readonly IArticleApplication _articleApplication;

        public LastArticleViewComponent(IArticleApplication articleApplication)
        {
            _articleApplication = articleApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var _model =await _articleApplication.GetForLastArticle();
            return View(_model);
        }

    }
}