using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Application;

namespace Fsn.Application.Contracts.Article
{
    public class CreateArticleCategory
    {
        public string Title { get; set; }
    }

    public class CategoryForEdit
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }

    public class ArticleCategory
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public int ArticleCount  { get; set; }

    }

    public class ListArticleCategory : BasePaging
    {
        public List<ArticleCategory> ArticleCategories { get; set; }
    }
}
