using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fsn.Domain.Account.AccessLevel;
using Fsn.Domain.Account.RoleAgg;
using Fsn.Domain.Account.UserAgg;
using Fsn.Infrastructure.EF.Context;
using Fsn.Infrastructure.EF.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Fsn.Infrastructure.EF.Data
{
    public class AddData_Main
    {
        private readonly IRoleRepo _roleRepo;
        private readonly IAccessLevelRepo _accessLevelRepo;
        private readonly IUserRepo _userRepo;
        private readonly MainContext _mainContext;


        public AddData_Main(IRoleRepo roleRepo, IAccessLevelRepo accessLevelRepo, IUserRepo userRepo, MainContext mainContext)
        {
            _roleRepo = roleRepo;
            _accessLevelRepo = accessLevelRepo;
            _userRepo = userRepo;
            _mainContext = mainContext;
        }

        public void Run()
        {
            try
            { 
                new AddData_Roles(_roleRepo).Run();
                new AddData_AccessLevel(_accessLevelRepo,_roleRepo).Run();
                new AddData_User(_userRepo, _accessLevelRepo, _roleRepo, _mainContext).Run();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

