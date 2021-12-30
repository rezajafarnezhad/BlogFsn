using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Fsn.Domain.Account.AccessLevel;
using Fsn.Domain.Account.RoleAgg;
using Fsn.Domain.Account.UserAgg;
using Fsn.Infrastructure.EF.Context;
using Microsoft.AspNetCore.Identity;

namespace Fsn.Infrastructure.EF.Data
{
    public class AddData_User
    {
        private readonly IUserRepo _userRepo;
        private readonly IAccessLevelRepo _accessLevelRepo;
        private readonly IRoleRepo _roleRepo;
        private MainContext _mainContext;
        public AddData_User(IUserRepo userRepo, IAccessLevelRepo accessLevelRepo, IRoleRepo roleRepo, MainContext mainContext)
        {
            _userRepo = userRepo;
            _accessLevelRepo = accessLevelRepo;
            _roleRepo = roleRepo;
            _mainContext = mainContext;
        }

        public void Run()
        {
            if (!_userRepo.Get.Any(c => c.UserName == "jafarnezhad@gmail.com"))
            {

                var _userid = new Guid().SequentialGuid();

                _userRepo.CreateAsync(new TUser()
                {
                    Id = _userid,
                    AccessLevelId = _accessLevelRepo.Get.Where(a => a.Name == "مدیرکل").Select(a => a.Id).Single(),
                    FullName = "محمدرضا جعفرنژاد",
                    IsActive = true,
                    CreateionData = DateTime.Now,
                    UserName = "jafarnezhad@gmail.com",
                    Email = "jafarnezhad@gmail.com",
                    NormalizedEmail = "jafarnezhad@gmail.com".ToUpper(),
                    NormalizedUserName = "RezaJafary".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEPvLer2b1K5QT7E8AUj/6twWXQxdSpfx9nIsPqK+O9Jku7nsgLF1cCM5xkMf/V8XLw==",
                    SecurityStamp = "QHZXXDN4PZUNNXGC6LQRVNOZ5EGGIKWH",
                    ConcurrencyStamp = "37116a3b-0da5-460e-b266-d5243f62e5c8",
                    PhoneNumber = "09126659595",
                    PhoneNumberConfirmed = true,
                }).Wait();

                foreach (var item in _roleRepo.Get.ToList())
                {
                    _mainContext.Add(new IdentityUserRole<Guid>()
                    {
                        RoleId = item.Id,
                        UserId = _userid,
                    });
                }
                _mainContext.SaveChanges();
            }
        }


    }
}
