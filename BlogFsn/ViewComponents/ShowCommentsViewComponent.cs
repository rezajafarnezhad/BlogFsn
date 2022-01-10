using System.Threading.Tasks;
using Fsn.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogFsn.ViewComponents
{
    public class ShowCommentsViewComponent : ViewComponent
    {
        private readonly ICommentApplication _commentApplication;
        public ShowCommentsViewComponent(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var _model = await _commentApplication.ShowByArticleId(id);
            return View(_model);
        }

    }
}