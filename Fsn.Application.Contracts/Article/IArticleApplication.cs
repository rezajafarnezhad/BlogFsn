using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Application;

namespace Fsn.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        Task<ListArticle> GetAll(int pageid, int take, string filter);
        Task<OperationResult> Create(CreateArticle createArticle);
        Task<Edit> GetForEdit(Guid Id);
        Task<OperationResult> Edit(Edit edit);
        Task<OperationResult> ChangeStatus(Guid Id);
        Task<OperationResult> Delete(Guid Id);
    }
}
