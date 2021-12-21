using Framework.Infrastructure;
using Fsn.Domain.Blog;
using Fsn.Infrastructure.EF.Context;

namespace Fsn.Infrastructure.EF.Repository
{
    public class ArticleCategoryRepo : BaseRepo<TArticleCategory>, IArticleCategoryRepo
    {
        public ArticleCategoryRepo(MainContext context) : base(context)
        {
        }
    }
}