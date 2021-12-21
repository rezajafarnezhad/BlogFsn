using System;
using System.Reflection.Metadata;

namespace Fsn.Domain.Blog
{
    public class TArticle
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid ArticleCategoryId { get; set; }
        public TArticleCategory ArticleCategory { get; set; }
    }
}