using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogFsn.Authentication;
using BlogFsn.Common.ExMethods;
using BlogFsn.Common.MessageBox;
using BlogFsn.Common.Types;
using BlogFsn.Models.ViewInput;
using Fsn.Application.Contracts.User;
using Fsn.Application.Interfaces;

namespace BlogFsn.Controllers
{
    public class AccountController : Controller
    {

        private readonly ImsgBox _msgBox;
        private readonly IUserApplication _userApplication;
        private readonly IJWTBuilder _jwtBuilder;
        public AccountController(ImsgBox msgBox, IUserApplication userApplication, IJWTBuilder jwtBuilder)
        {
            _msgBox = msgBox;
            _userApplication = userApplication;
            _jwtBuilder = jwtBuilder;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewInput _createUser)
        {
            if (!ModelState.IsValid)
                return _msgBox.ModelStateMsg(ModelState.GetErrors());

            var CreateUser = new CreateUser()
            {
                FullName = _createUser.FullName,
                Email = _createUser.Email,
                Password = _createUser.Password
            };

            var result = await _userApplication.Register(CreateUser);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message,"GotoIndex()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message.Replace(", ","</br>"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> EmailConfirmation(string Token)
        {
            if (string.IsNullOrWhiteSpace(Token))
            {
                return NotFound();
            }

            var result = await _userApplication.EmailConfirmation(Token);
            if (result.isSucceeded)
            {
                ViewBag.Status = true;
                ViewBag.Message = result.Message;
                return View();
            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Message = result.Message.Replace(", ","</br>");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl ?? "/Home/Index";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewInput loginViewInput)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());

            var loginUser = new LoginUser()
            {
                Email = loginViewInput.Email,
                Password = loginViewInput.Password
            };

            var result = await _userApplication.Login(loginUser);
            if (result.isSucceeded)
            {

                string GeneratedToken = await _jwtBuilder.CreateTokenAsync(result.Message);

                // Create Cookie
                Response.CreateAuthCookie(GeneratedToken, loginViewInput.RemmemberMe);

                return new JsResult("GotoReturnUrl()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }

        }

        [HttpGet("/401")]
        public async Task<IActionResult> Error401()
        {
            return View();
        }
        [HttpGet("/403")]
        public async Task<IActionResult> Error403()
        {
            return View();
        }
        [HttpGet("/404")]
        public async Task<IActionResult> Error404()
        {
            return View();
        }
        [HttpGet("/500")]
        public async Task<IActionResult> Error500()
        {
            return View();
        }

    }
}
