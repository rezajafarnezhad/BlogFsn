using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Application;
using Fsn.Application.Contracts.Article;
using Fsn.Application.Contracts.Commnet;

namespace Fsn.Application.Interfaces
{
    public interface ICommentApplication
    {
        Task<OperationResult> CreateCommnet(CreateCommnet commnet);
        Task<ListComment> GetAll(int pageId, int take, string Filter);
        Task<CommnetModel> Show(string Id);
        Task<OperationResult> Delete(string Id);
        Task<OperationResult> Confirm(string Id);
        Task<OperationResult> NotConfirm(string Id);
        Task<List<CommnetModel>> ShowByArticleId(string Id);
    }
}