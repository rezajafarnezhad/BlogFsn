using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsn.Domain.Blog
{
    public class TArticleCategory
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }

        public ICollection<TArticle> Articles { get; set; }
    }
}
