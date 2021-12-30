using System.Threading.Tasks;
using Framework.Application;

namespace Fsn.Application.Interfaces
{
    public interface IAccessLevelRoleApplication
    {
        Task<OperationResult> RemoveByAccessLevelId(string accessLevelId);
        Task<OperationResult> AddRolesToAccessLevele(string accessLevelId, string[] Roles);
    }
}