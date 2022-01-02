using System.Threading.Tasks;

namespace BlogFsn.Authentication
{
    public interface IJWTBuilder
    {
        Task<string> CreateTokenAsync(string userid);
    }
}