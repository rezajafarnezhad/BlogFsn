using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogFsn.Common.MessageBox;
using Fsn.Application.Interfaces;

namespace BlogFsn.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CommentController : Controller
    {

        private readonly ICommentApplication _commentApplication;
        private readonly ImsgBox _msgBox;

        public CommentController(ICommentApplication commentApplication, ImsgBox msgBox)
        {
            _commentApplication = commentApplication;
            _msgBox = msgBox;
        }

        public async Task<IActionResult> Index(int pageid = 1, int take = 10,string filter = "")
        {
            var _Comments = await _commentApplication.GetAll(pageid, take, filter);
            return View(_Comments);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int pageid, int take, string filter)
        {
            var _AllComments = await _commentApplication.GetAll(pageid, take, filter);
            return View("_Comments", _AllComments);
        }


        public async Task<IActionResult> Show(string id)
        {
            var _comment = await _commentApplication.Show(id);
            if (_comment is null)
                return NotFound();

            return View("_Show", _comment);
        }


        [HttpPost]
        public async Task<IActionResult> Confirm(string id)
        {
            var result = await _commentApplication.Confirm(id);
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

        public async Task<IActionResult> NotConfirm(string id)
        {
            var result = await _commentApplication.NotConfirm(id);
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

        public async Task<IActionResult> Delete(string id)
        {
            var result = await _commentApplication.Delete(id);
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
