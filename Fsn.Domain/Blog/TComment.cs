using System;
using System.Reflection.Metadata.Ecma335;

namespace Fsn.Domain.Blog
{
    public class TComment
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Message { get; set; }
        public int Statuse { get; set; }
        
        public Guid ArticleId { get; set; }
        public TArticle Article { get; set; }

    }
}