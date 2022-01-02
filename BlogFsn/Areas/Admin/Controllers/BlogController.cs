using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogFsn.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace BlogFsn.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminPanelPolicy")]
    [Area("Admin")]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
