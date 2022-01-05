using System;
using System.Collections.Generic;
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

        public async Task<OperationResult> RemoveAllRolesByUserAsync(TUser user)
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
                    return operationResult.Failed(string.Join(", ", result.Errors.Select(c => c.Description)));
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
                    return operationResult.Failed(string.Join(", ", result.Errors.Select(c =>  c.Description)));
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
                    var RemoveResult = await RemoveAllRolesByUserAsync(user);
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

        public async Task<OperationResult> CreateUser(CreateUser createUser)
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
                    UserName = createUser.Email,
                    EmailConfirmed = true,
                };

                var result = await _userRepo.CreateUser(_user, createUser.Password);
                if (result.Succeeded)
                {
                    return operationResult.Succeeded("ثبت نام شما با موفقیت انجام شد.");
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

        public async Task<EditUser> GetForEdit(string id)
        {
            try
            {
                var _user = await _userRepo.Get.Where(c => c.Id == Guid.Parse(id)).Select(c => new EditUser()
                {
                    Id = c.Id.ToString(),
                    FullName = c.FullName,
                    Email = c.Email,
                    Password = "",
                    ConfirmPassword = ""

                }).SingleOrDefaultAsync();

                return _user;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> Edit(EditUser editUser)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var _user = await _userRepo.GetById(Guid.Parse(editUser.Id));
                if (_user is null)
                    return operationResult.Failed();

                var result = await _userRepo.ChangePassword(_user, _user.PasswordHash, editUser.Password);
                if (result.Succeeded)
                {
                    _user.FullName = editUser.FullName;
                    _user.Email = editUser.Email;
                    _user.NormalizedUserName = editUser.Email.ToUpper();
                    _user.UserName = editUser.Email;
                    var result2 = await _userRepo.EditUser(_user);
                    if (result2.Succeeded)
                    {
                        return operationResult.Succeeded();
                    }
                    else
                    {
                        return operationResult.Failed(string.Join(", ", result.Errors.Select(c => c.Description)));

                    }
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
                var qUser = await _userRepo.Get.Include(c => c.TAccessLevel).Where(c => c.Id == Guid.Parse(userId)).Select(c =>
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

        public async Task<ListUsers> GetAllUser(int pageid, int take, string filter, string Fillter2)
        {
            try
            {
                var result = _userRepo.Get.Include(c => c.TAccessLevel).Select(c => new UserInfo()
                {
                    Id = c.Id.ToString(),
                    AccessLevelId = c.AccessLevelId.ToString(),
                    FullName = c.FullName,
                    IsActive = c.IsActive,
                    Date = c.CreateionData.ToFarsiFull(),
                    AccessLevelTile = c.TAccessLevel.Name,
                    Email = c.Email,
                    IsEmailConfirmed = c.EmailConfirmed
                });

                if (!string.IsNullOrWhiteSpace(filter))
                    result = result.Where(c => c.FullName.Contains(filter) || c.Email.Contains(filter));

                if (Fillter2 == "IsNotActive")
                    result = result.Where(c => c.IsActive == false);

                if (Fillter2 == "IsNotConfEmail")
                    result = result.Where(c => c.IsEmailConfirmed == false);


                var skip = (pageid - 1) * take;

                var model = new ListUsers()
                {
                    Filter = filter,
                    Fillter2 = Fillter2,
                    UserInfos = await result.OrderByDescending(c => c.Id).Skip(skip).Take(take).ToListAsync(),
                };

                model.GenaratPaging(result, pageid, take);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> ChangeStatus(string id, string operatorId)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _user = await _userRepo.GetById(Guid.Parse(id));

                if (_user is null)
                    return operationResult.Failed();

                if (_user.Id == Guid.Parse(operatorId))
                    return operationResult.Failed("شما نمی توانید وضعیت خود را تغییر دهید");

                if (_user.AccessLevelId == await _accessLevelApplication.GetIdByName("مدیرکل"))
                    return operationResult.Failed("شما نمی توانید وضعیت مدیرکل را تغییر دهید");

                _user.IsActive = !_user.IsActive;
                await _userRepo.UpdateAsync(_user);
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<OperationResult> Delete(string id, string operatorId)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _user = await _userRepo.GetById(Guid.Parse(id));

                if (_user is null)
                    return operationResult.Failed();

                if (_user.Id == Guid.Parse(operatorId))
                    return operationResult.Failed("شما نمی توانید خود را حذف کنید");


                if (_user.AccessLevelId == await _accessLevelApplication.GetIdByName("مدیرکل"))
                    return operationResult.Failed("شما نمی توانید مدیرکل را حذف کنید");

                await _userRepo.DeleteAsync(_user);
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<UserAccessLevel> GetForChangeUserAcc(string id)
        {
            try
            {
                var qdata = await _userRepo.Get.Where(c => c.Id == Guid.Parse(id)).Select(c => new UserAccessLevel()
                {
                    Id = c.Id.ToString(),
                    AccessLevelId = c.AccessLevelId.ToString(),
                    FullName = c.FullName,

                }).SingleOrDefaultAsync();

                return qdata;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<OperationResult> ChangeUserAccessLevel(string userId, string operatorId, string accessLevelId)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(operatorId) ||
                    string.IsNullOrWhiteSpace(accessLevelId))
                {
                    return operationResult.Failed();
                }

                var _User = await _userRepo.GetById(Guid.Parse(userId));

                if (_User is null)
                    return operationResult.Failed();

                if (_User.Id == Guid.Parse(operatorId))
                    return operationResult.Failed("شما نمی توانید سطح دسترسی خود را تغییر دهید");


                _User.AccessLevelId = Guid.Parse(accessLevelId);

                await _userRepo.UpdateAsync(_User);

                var result = await RemoveAllRolesByUserAsync(_User);
                if (result.isSucceeded)
                {
                    var _rolesName = await _accessLevelApplication.GetRolesNameByAccessLevelId(accessLevelId);
                    var result2 = await AddRoles(_User, _rolesName);
                    if (result2.isSucceeded)
                    {
                        return operationResult.Succeeded();
                    }
                    else
                    {
                        return operationResult.Failed(result.Message);

                    }
                }
                else
                {
                    return operationResult.Failed(result.Message);
                }



            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<List<string>> GetUserIdsByAcccessLevelId(string accessLevelId)
        {
            try
            {
                var qdata = await _userRepo.Get.Where(c => c.AccessLevelId == Guid.Parse(accessLevelId)).Select(c=>c.Id.ToString()).ToListAsync();
                return qdata;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}