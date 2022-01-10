using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Application;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Fsn.Application.Contracts.Commnet
{
    public class ListComment:BasePaging
    {

        public List<CommnetModel> CommnetModels { get; set; }
        public string Filter { get; set; }
    }

    public class CommnetModel
    {
        public string Id { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ArticleTitle { get; set; }
        public int Status { get; set; }
        public string Date { get; set; }
    }

    public class CreateCommnet
    {
        public string UserId { get; set; }
        public string ArticleId { get; set; }
        public string Message { get; set; }
    }
}
