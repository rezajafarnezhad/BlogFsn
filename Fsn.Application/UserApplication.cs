using System;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Framework.Application;
using Framework.Application.Consts;
using Framework.Infrastructure;
using Framework.Infrastructure.Email;
using Fsn.Application.Contracts.User;
using Fsn.Application.Interfaces;
using Fsn.Domain.Account.UserAgg;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepo _userRepo;
        private readonly IAccessLevelApplication _accessLevelApplication;
        private readonly IEmailSender _emailSender;
        public UserApplication(IUserRepo userRepo, IAccessLevelApplication accessLevelApplication, IEmailSender emailSender)
        {
            _userRepo = userRepo;
            _accessLevelApplication = accessLevelApplication;
            _emailSender = emailSender;
        }

        public async Task<OperationResult> RemoveAllRolesByUserIdAsync(TUser user)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var result = await _userRepo.RemoveAllRolesByUserAsync(user);
                if (result.Succeeded)
                {
                    return operationResult.Succeeded();
                }
                else
                {
                    return operationResult.Failed(string.Join(" | ", result.Errors.Select(c => new { errtxt = c.Code + "-" + c.Description })));
                }

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<OperationResult> AddRoles(TUser user, string[] Roles)
        {
            OperationResult operationResult = new OperationResult();

            try
            {

                var result = await _userRepo.AddToRolesAsync(user, Roles);
                if (result.Succeeded)
                {
                    return operationResult.Succeeded();
                }
                else
                {
                    return operationResult.Failed();
                }

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<OperationResult> EditUsersRoleByAccId(string accessLevelId, string[] Roles)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _User = await _userRepo.Get.Where(c => c.AccessLevelId == Guid.Parse(accessLevelId)).ToListAsync();
                foreach (var user in _User)
                {
                    //Delete membership roles
                    var RemoveResult = await RemoveAllRolesByUserIdAsync(user);
                    if (RemoveResult.isSucceeded)
                    {
                        //Add Roles
                        var AddResult = await AddRoles(user, Roles);
                        if (AddResult.isSucceeded == false)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }

                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<OperationResult> Register(CreateUser createUser)
        {
            OperationResult operationResult = new OperationResult();
            try
            {

                var _user = new TUser()
                {
                    Email = createUser.Email,
                    FullName = createUser.FullName,
                    CreateionData = DateTime.Now,
                    IsActive = true,
                    AccessLevelId = await _accessLevelApplication.GetIdByName("کاربر"),
                    UserName = createUser.Email
                };

                var result = await _userRepo.CreateUser(_user, createUser.Password);
                if (result.Succeeded)
                {
                    //Send Email

                    string _userId = _user.Id.ToString();
                    string _Token = await GenerateEmailConfirmationToken(_user);
                    string EncToken = $"{_userId}, {_Token}".AesEncrypt(AuthConst.SecretKey);
                    string SiteUrl = "https://localhost:44352";
                    string CallbackUrl = $"{SiteUrl}/Account/EmailConfirmation?Token={WebUtility.UrlEncode(EncToken)}";
                    string Body = $"برای تایید ایمیل کلیک کنید </br> <a href='{CallbackUrl}'>Click for Confirm Email</a>";
                    await _emailSender.Send(_user.Email, Body, "فعال سازی حساب FSnBlog");
                    return operationResult.Succeeded("ثبت نام شما با موفقیت انجام شد. جهت فعال سازی حساب خود به ایمیل خود مراجه کنید و بر روی لینگ فعال سازی کلیک نمایید.");
                }
                else
                {
                    return operationResult.Failed(string.Join(", ", result.Errors.Select(c => c.Description)));
                }

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<string> GenerateEmailConfirmationToken(TUser user)
        {
            return await _userRepo.GenerateEmailConfirmationToken(user);
        }
        public async Task<OperationResult> EmailConfirmation(string token)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                string _Dectoken = token.AesDecrypt(AuthConst.SecretKey);
                string _userId = _Dectoken.Split(", ")[0];
                string _token = _Dectoken.Split(", ")[1];

                var qUser = await _userRepo.GetById(Guid.Parse(_userId));

                var result = await _userRepo.EmailConfirmation(qUser, _token);
                if (result.Succeeded)
                {
                    return operationResult.Succeeded("حساب شما با موفقیت فعال گردید ");
                }
                else
                {
                    return operationResult.Failed(string.Join(", ", result.Errors.Select(c => c.Description)));
                }
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }
        public async Task<OperationResult> Login(LoginUser loginUser)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var quser = await _userRepo.GetUserByEmail(loginUser.Email);
                if (quser is null)
                    return operationResult.Failed("اطلاعات وارد شده اشتباه میباشد");
                if (quser.EmailConfirmed == false)
                    return operationResult.Failed("حساب کاربری شما فعال نیست جهت فعال سازی حساب خود به ایمیل خود مراجه کنید و بر روی لینگ فعال سازی کلیک نمایید اگر ایمیلی دریافت نکرده اید دوباره ثبت نام کنید");
                if (quser.IsActive == false)
                    return operationResult.Failed("حساب کاربری شما غیر فعال شده است");

                var result = await _userRepo.PasswordSignIn(quser, loginUser.Password, true, true);
                if (result.Succeeded)
                {
                    return operationResult.Succeeded(quser.Id.ToString());
                }
                else
                {
                    if (result.IsLockedOut)
                        return operationResult.Failed("حساب شما قفل شده");

                    else if (result.IsNotAllowed)
                        return operationResult.Failed("حساب شما قفل شده");
                    else
                        return operationResult.Failed("اطلاعات وارد شده اشتباه میباشد");
                }

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<OutGetAllUserDetails> GetAllUserDetailsAsync(string userId)
        {
            try
            {
                var qUser = await _userRepo.Get.Include(c=>c.TAccessLevel).Where(c => c.Id == Guid.Parse(userId)).Select(c =>
                    new OutGetAllUserDetails()
                    {
                        Id = c.Id.ToString(),
                        Email = c.Email,
                        UserName = c.UserName,
                        FullName = c.FullName,
                        IsActive = c.IsActive,
                        Data = c.CreateionData,
                        AccessLevelId = c.AccessLevelId.ToString(),
                        AccessLevelTile = c.TAccessLevel.Name

                    }).SingleOrDefaultAsync();

                return qUser;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TUser> GetUserById(string userid)
        {
            try
            {
                return await _userRepo.GetById(Guid.Parse(userid));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}