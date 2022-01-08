using System;
using System.Threading.Tasks;
using Framework.Application;
using Fsn.Application.Contracts.Article;

namespace Fsn.Application.Interfaces
{
    public interface IArticleApplication
    {
        Task<ListArticle> GetAll(int pageid, int take, string filter,string CategorySearch);
        Task<ListArticle> GetAllInIndexPage(int pageid, int take, string filter);
        Task<OperationResult> Create(CreateArticle createArticle);
        Task<Edit> GetForEdit(Guid Id);
        Task<OperationResult> Edit(Edit edit);
        Task<OperationResult> ChangeStatus(Guid Id);
        Task<OperationResult> Delete(Guid Id);
        Task<ListArticle> GetArticleByCategoryTitle(int pageid, int take, string Title);
    }
}
