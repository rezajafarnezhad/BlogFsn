using BlogFsn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Fsn.Domain.Account.AccessLevel;
using Fsn.Domain.Account.RoleAgg;
using Fsn.Domain.Account.UserAgg;
using Fsn.Infrastructure.EF.Context;
using Fsn.Infrastructure.EF.Data;

namespace BlogFsn.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoleRepo _roleRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAccessLevelRepo _accessLevelRepo;
        private readonly MainContext _mainContext;

        public HomeController(IRoleRepo roleRepo, IUserRepo userRepo, IAccessLevelRepo accessLevelRepo, MainContext mainContext)
        {
            _roleRepo = roleRepo;
            _userRepo = userRepo;
            _accessLevelRepo = accessLevelRepo;
            _mainContext = mainContext;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("/AddData")]
        public IActionResult AddData()
        {
            new AddData_Main(_roleRepo,_accessLevelRepo,_userRepo,_mainContext).Run();
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
