using System;
using System.Threading.Tasks;
using Framework.Application;
using Fsn.Application.Contracts.AccessLevel;

namespace Fsn.Application.Interfaces
{
    public interface IAccessLevelApplication
    {
        Task<ListAccessLevels> GetAll(int pageId , int take , string filter);
        Task<OperationResult> Create(CreateAccesslevel accesslevel);
        Task<EditAccesslevel> GetForEdit(string Id);
        Task<OperationResult> Edit(EditAccesslevel accesslevel);
        Task<OperationResult> Delete(string Id);
        Task<Guid> GetIdByName(string name);
    }
}
