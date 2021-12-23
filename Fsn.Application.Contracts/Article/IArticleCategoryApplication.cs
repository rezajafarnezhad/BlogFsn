using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Application;

namespace Fsn.Application.Contracts.Article
{
    public interface IArticleCategoryApplication
    {
        Task<OperationResult> Create(CreateArticleCategory category);
        Task<ListArticleCategory> GetAll(int pageid, int take);
        Task<CategoryForEdit> GetForEdit(Guid? Id);
        Task<OperationResult> Edit(CategoryForEdit categoryForEdit);
        Task<OperationResult> Delete(Guid Id);
        Task<OperationResult> ChangeStatuse(Guid Id);
        Task<List<ArticleCategory>> GetForArticle();
    }
}