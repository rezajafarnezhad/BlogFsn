using System;
using System.Linq;
using Framework.Infrastructure;
using Fsn.Domain.Account.RoleAgg;
using Fsn.Infrastructure.EF.Context;

namespace Fsn.Infrastructure.EF.Data
{
    public class AddData_Roles
    {

        private readonly IRoleRepo _repRoles;
        public AddData_Roles(IRoleRepo repRoles)
        {
            _repRoles = repRoles;
        }
        public void Run()
        {

            if (!_repRoles.Get.Any(a => a.Name == "AdminPage"))
            {
                _repRoles.CreateAsync(new TRole()
                {
                    Id = new Guid().SequentialGuid(),
                    ParentId = null,
                    PageName = "دسترسی به پنل مدیریت",
                    Sort = 0,
                    Name = "AdminPage",
                    NormalizedName = "AdminPage".ToUpper(),
                    Description = "دسترسی به پنل مدیریت"
                }).Wait();
            }


            #region ManageAccessLevelPage
            {
                Guid _Id = new Guid().SequentialGuid();
                if (!_repRoles.Get.Any(a => a.Name == "CanManageAccessLevel"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "ManageAccessLevelPage",
                        Sort = 10,
                        Name = "CanManageAccessLevel",
                        NormalizedName = "CanManageAccessLevel".ToUpper(),
                        Description = "توانایی مدیریت سطوح دسترسی"
                    }).Wait();
                }
                else
                {
                    _Id = _repRoles.Get.Where(a => a.Name == "CanManageAccessLevel").Select(a => a.Id).Single();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListAccessLevel"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageAccessLevelPage",
                        Sort = 20,
                        Name = "CanViewListAccessLevel",
                        NormalizedName = "CanViewListAccessLevel".ToUpper(),
                        Description = "توانایی مشاهده لیست سطوح دسترسی"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanAddAccessLevel"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageAccessLevelPage",
                        Sort = 30,
                        Name = "CanAddAccessLevel",
                        NormalizedName = "CanAddAccessLevel".ToUpper(),
                        Description = "توانایی افزودن سطح دسترسی"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanEditAccessLevel"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageAccessLevelPage",
                        Sort = 40,
                        Name = "CanEditAccessLevel",
                        NormalizedName = "CanEditAccessLevel".ToUpper(),
                        Description = "توانایی ویرایش سطح دسترسی"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanRemoveAccessLevel"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageAccessLevelPage",
                        Sort = 50,
                        Name = "CanRemoveAccessLevel",
                        NormalizedName = "CanRemoveAccessLevel".ToUpper(),
                        Description = "توانایی حذف سطح دسترسی"
                    }).Wait();
                }
            }
            #endregion


            #region ManageCategories
            {
                Guid _Id = new Guid().SequentialGuid();
                if (!_repRoles.Get.Any(a => a.Name == "CanManageCategories"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "ManageCategoriesPage",
                        Sort = 60,
                        Name = "CanManageCategories",
                        NormalizedName = "CanManageCategories".ToUpper(),
                        Description = "توانایی مدیریت دسته بندی ها"
                    }).Wait();
                }
                else
                {
                    _Id = _repRoles.Get.Where(a => a.Name == "CanManageCategories").Select(a => a.Id).Single();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListCategories"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageCategoriesPage",
                        Sort = 70,
                        Name = "CanViewListCategories",
                        NormalizedName = "CanViewListCategories".ToUpper(),
                        Description = "توانایی مشاهده لیست دسته بندی ها"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanAddCategory"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageCategoriesPage",
                        Sort = 80,
                        Name = "CanAddCategory",
                        NormalizedName = "CanAddCategory".ToUpper(),
                        Description = "توانایی افزودن دسته بندی جدید"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanEditCategory"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageCategoriesPage",
                        Sort = 90,
                        Name = "CanEditCategory",
                        NormalizedName = "CanEditCategory".ToUpper(),
                        Description = "توانایی ویرایش دسته بندی"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanRemoveCategory"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageCategoriesPage",
                        Sort = 100,
                        Name = "CanRemoveCategory",
                        NormalizedName = "CanRemoveCategory".ToUpper(),
                        Description = "توانایی حذف دسته بندی"
                    }).Wait();
                }
                //CanChangeStatusCategory
                if (!_repRoles.Get.Any(a => a.Name == "CanChangeStatusCategory"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageCategoriesPage",
                        Sort = 110,
                        Name = "CanChangeStatusCategory",
                        NormalizedName = "CanChangeStatusCategory".ToUpper(),
                        Description = "توانایی تغییر وضعیت دسته بندی"
                    }).Wait();
                }
            }
            #endregion


            #region ManageArticle
            {
                Guid _Id = new Guid().SequentialGuid();
                if (!_repRoles.Get.Any(a => a.Name == "CanManageArticle"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "ManageArticle",
                        Sort = 120,
                        Name = "CanManageArticle",
                        NormalizedName = "CanManageArticle".ToUpper(),
                        Description = "توانایی مدیریت مقالات "
                    }).Wait();
                }
                else
                {
                    _Id = _repRoles.Get.Where(a => a.Name == "CanManageArticle").Select(a => a.Id).Single();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListArticle"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageArticle",
                        Sort = 130,
                        Name = "CanViewListArticle",
                        NormalizedName = "CanViewListArticle".ToUpper(),
                        Description = "توانایی مشاهده لیست مقالات"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanAddArticle"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageArticle",
                        Sort = 140,
                        Name = "CanAddArticle",
                        NormalizedName = "CanAddArticle".ToUpper(),
                        Description = "توانایی افزودن مقاله جدید"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanEditArticle"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageArticle",
                        Sort = 150,
                        Name = "CanEditArticle",
                        NormalizedName = "CanEditArticle".ToUpper(),
                        Description = "توانایی ویرایش مقاله"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanRemoveArticle"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageArticle",
                        Sort = 160,
                        Name = "CanRemoveArticle",
                        NormalizedName = "CanRemoveArticle".ToUpper(),
                        Description = "توانایی حذف مقاله"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanChangeStatusArticle"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageArticle",
                        Sort = 170,
                        Name = "CanChangeStatusArticle",
                        NormalizedName = "CanChangeStatusArticle".ToUpper(),
                        Description = "توانایی تغییر وضعیت مقاله"
                    }).Wait();
                }
            }
            #endregion

            #region ManageUsersPage
            {
                Guid _Id = new Guid().SequentialGuid();
                if (!_repRoles.Get.Any(a => a.Name == "CanManageUsers"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "ManageUsersPage",
                        Sort = 190,
                        Name = "CanManageUsers",
                        NormalizedName = "CanManageUsers".ToUpper(),
                        Description = "توانایی مدیریت کاربران"
                    }).Wait();
                }
                else
                {
                    _Id = _repRoles.Get.Where(a => a.Name == "CanManageUsers").Select(a => a.Id).Single();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListUsers"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 200,
                        Name = "CanViewListUsers",
                        NormalizedName = "CanViewListUsers".ToUpper(),
                        Description = "توانایی مشاهده لیست کاربران"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanAddUsers"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 210,
                        Name = "CanAddUsers",
                        NormalizedName = "CanAddUsers".ToUpper(),
                        Description = "توانایی افزودن کاربر جدید"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanEditUsers"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 220,
                        Name = "CanEditUsers",
                        NormalizedName = "CanEditUsers".ToUpper(),
                        Description = "توانایی ویرایش کاربر"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanRemoveUsers"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 230,
                        Name = "CanRemoveUsers",
                        NormalizedName = "CanRemoveUsers".ToUpper(),
                        Description = "توانایی حذف کاربر"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanChangeUsersStatus"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 240,
                        Name = "CanChangeUsersStatus",
                        NormalizedName = "CanChangeUsersStatus".ToUpper(),
                        Description = "توانایی تغییر وضعیت کاربر"
                    }).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanChangeUsersAccessLevel"))
                {
                    _repRoles.CreateAsync(new TRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 250,
                        Name = "CanChangeUsersAccessLevel",
                        NormalizedName = "CanChangeUsersAccessLevel".ToUpper(),
                        Description = "توانایی تغییر سطح دسترسی کاربر"
                    }).Wait();
                }
            }
            #endregion

            _repRoles.SaveChangeAsync().Wait();
        }
    }
}