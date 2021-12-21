using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Fsn.Domain.Blog;
using Fsn.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Infrastructure.EF.Repository
{
    public class ArticleRepo : BaseRepo<TArticle>, IArticleRepo 
    {
        public ArticleRepo(MainContext context) : base(context)
        {
        }
    }
}
