using Fsn.Domain.Account.UserAgg;

namespace Fsn.Application.Contracts.User
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepo _userRepo;
        public UserApplication(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
    }
}