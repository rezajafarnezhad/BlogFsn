using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Application;
using Framework.Infrastructure;
using Fsn.Application.Contracts.AccessLevel;
using Fsn.Application.Interfaces;
using Fsn.Domain.Account.AccessLevel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Fsn.Application
{
    public class AccessLevelApplication : IAccessLevelApplication
    {
        private readonly IAccessLevelRepo _accessLevelRepo;
        private readonly IAccessLevelRoleApplication _accessLevelRoleApplication;
        public AccessLevelApplication(IAccessLevelRepo accessLevelRepo, IAccessLevelRoleApplication accessLevelRoleApplication)
        {
            _accessLevelRepo = accessLevelRepo;
            _accessLevelRoleApplication = accessLevelRoleApplication;
        }


        public async Task<ListAccessLevels> GetAll(int pageId, int take, string filter)
        {
            var Result = _accessLevelRepo.Get.Select(c => new AccessLevels()
            {
                Id = c.Id.ToString(),
                Title = c.Name,
                Count = c.TUsers.Count
            });

            if (!string.IsNullOrWhiteSpace(filter))
                Result = Result.Where(c => c.Title.Contains(filter));

            var skip = (pageId - 1) * take;

            var model = new ListAccessLevels()
            {
                Filter = filter,
                AccessLevels = await Result.OrderByDescending(c => c.Title).Skip(skip).Take(take).ToListAsync(),
            };

            model.GenaratPaging(Result, pageId, take);
            return model;

        }

        public async Task<OperationResult> Create(CreateAccesslevel accesslevel)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                if (await _accessLevelRepo.Get.AnyAsync(c => c.Name == accesslevel.Title))
                    return operationResult.Failed("این عنوان در سیستم وجود دارد");

                var AccessLevel = new TAccessLevel()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = accesslevel.Title,
                    TAccessLevelRoles = accesslevel.Roles.Select(RoleId => new TAccessLevelRole()
                    {
                        Id = new Guid().SequentialGuid(),
                        RoleId = Guid.Parse(RoleId)
                    }).ToList()
                };

                await _accessLevelRepo.CreateAsync(AccessLevel);
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<Guid> GetIdByName(string name)
        {

            return await _accessLevelRepo.Get.Where(c => c.Name == name).Select(c => c.Id).SingleOrDefaultAsync();

        }

        public async Task<EditAccesslevel> GetForEdit(string Id)
        {
            var AccessLevel = await _accessLevelRepo.Get.Where(c => c.Id == Guid.Parse(Id)).Select(c => new EditAccesslevel()
            {
                Id = c.Id.ToString(),
                Title = c.Name,
                Roles = null,

            }).SingleOrDefaultAsync();

            return AccessLevel;
        }

        public async Task<OperationResult> Edit(EditAccesslevel accesslevel)
        {
            OperationResult operationResult = new OperationResult();

            var _AccessLevel = await _accessLevelRepo.GetById(Guid.Parse(accesslevel.Id));
            if (_AccessLevel == null)
                return operationResult.Failed();

            _AccessLevel.Name = accesslevel.Title;

            //Delete Roles

            var result = await _accessLevelRoleApplication.RemoveByAccessLevelId(accesslevel.Id);
            if (result.isSucceeded)
            {
                var result2 = await _accessLevelRoleApplication.AddRolesToAccessLevele(accesslevel.Id, accesslevel.Roles);
                if (result2.isSucceeded)
                {
                    await _accessLevelRepo.UpdateAsync(_AccessLevel);
                    return operationResult.Succeeded();
                }
                return operationResult.Failed();
            }
            else
            {
                return operationResult.Failed();
            }
        }

        public async Task<OperationResult> Delete(string Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var AccessLevel = await _accessLevelRepo.GetById(Guid.Parse(Id));

                if (AccessLevel is null)
                    return operationResult.Failed();

                //Number Of Users
                int numberofusers = await _accessLevelRepo.Get.Where(c => c.Id == Guid.Parse(Id)).Select(c => c.TUsers.Count).SingleOrDefaultAsync();

                if (numberofusers > 0)
                    return operationResult.Failed("کاربرانی عضو این سطح هستند امکان حذف این سطح وجود ندارد");

                await _accessLevelRepo.DeleteAsync(AccessLevel);
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

    }
}
