using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Framework.Application;
using Microsoft.AspNetCore.Http;

namespace Fsn.Application.Contracts.Article
{
    public class CreateArticle
    {
        [Required]
        [DisplayName("عنوان")]

        public string Title { get; set; }
        [Required]
        [DisplayName("عکس")]

        public IFormFile ImagFile { get; set; }
        [Required]
        [DisplayName("محتوا")]
        public string Content { get; set; }
        [Required]
        [DisplayName("دسته")]

        public Guid ArticleCategoryId { get; set; }
        public string CreationDate { get; set; }
    }

    public class Edit
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        [Required]
        [DisplayName("عنوان")]

        public string Title { get; set; }

        public IFormFile ImagFile { get; set; }
        [Required]
        [DisplayName("محتوا")]
        public string Content { get; set; }
        [Required]
        [DisplayName("دسته")]

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
        public string CategorySearch { get; set; }
    }

    public class LastArticle
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int  CommentCount { get; set; }
        public string Content { get; set; }
    }

    public class SingleArticle : LastArticle
    {

    }
}
