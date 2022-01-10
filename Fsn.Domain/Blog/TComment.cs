using System;
using System.Reflection.Metadata.Ecma335;
using Fsn.Domain.Account.UserAgg;

namespace Fsn.Domain.Blog
{
    public class TComment
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public DateTime CreateionDate { get; set; }
        public Guid UserId { get; set; }
        public Guid ArticleId { get; set; }
        public TArticle Article { get; set; }
        public TUser User { get; set; }
        
    }

    public enum StatusComment
    {
        New,
        Confirmed,
        Cancel,
    }
}