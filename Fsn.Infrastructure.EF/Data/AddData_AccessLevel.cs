using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Fsn.Domain.Account.AccessLevel;
using Fsn.Domain.Account.RoleAgg;

namespace Fsn.Infrastructure.EF.Data
{
    public class AddData_AccessLevel
    {
        private readonly IAccessLevelRepo _accessLevelRepo;
        private readonly IRoleRepo _roleRepo;
        public AddData_AccessLevel(IAccessLevelRepo accessLevelRepo, IRoleRepo roleRepo)
        {
            _accessLevelRepo = accessLevelRepo;
            _roleRepo = roleRepo;
        }

        public void Run()
        {

            if (!_accessLevelRepo.Get.Any(c => c.Name == "مدیرکل"))
            {
                var AccessLevel = new TAccessLevel()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "مدیرکل",
                    TAccessLevelRoles = new List<TAccessLevelRole>(),
                };
                foreach (var Item in _roleRepo.Get.ToList())
                {
                    AccessLevel.TAccessLevelRoles.Add(new TAccessLevelRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        RoleId = Item.Id,
                        AccessLevelId = AccessLevel.Id
                    });
                }
                _accessLevelRepo.CreateAsync(AccessLevel).Wait();
            }

            if (!_accessLevelRepo.Get.Any(c => c.Name == "کاربر"))
            {

                _accessLevelRepo.CreateAsync(new TAccessLevel()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "کاربر"
                }).Wait();
            }

            _accessLevelRepo.SaveChangeAsync().Wait();

        }
    }
}
