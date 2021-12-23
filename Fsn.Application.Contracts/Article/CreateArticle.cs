using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Application;
using Microsoft.AspNetCore.Http;

namespace Fsn.Application.Contracts.Article
{
    public class CreateArticle
    {
        public string Title { get; set; }
        public IFormFile ImagFile { get; set; }
        public string Content { get; set; }
        public Guid ArticleCategoryId { get; set; }
        public string CreationDate { get; set; }
    }

    public class ArticleS
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public Guid ArticleCategoryId { get; set; }
        public string ArticleCategoryName { get; set; }
    }

    public class ListArticle : BasePaging
    {
        public List<ArticleS> ArticleS { get; set; }
        public string Fillter { get; set; }
    }
}
