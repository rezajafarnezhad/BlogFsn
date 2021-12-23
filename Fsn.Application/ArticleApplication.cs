using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application;
using Fsn.Application.Contracts.Article;
using Fsn.Domain.Blog;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Application
{
    public class ArticleApplication : IArticleApplication
    {

        private readonly IArticleRepo _articleRepo;
        private readonly IUploadFile _uploadFile;
        public ArticleApplication(IArticleRepo articleRepo, IUploadFile uploadFile)
        {
            _articleRepo = articleRepo;
            _uploadFile = uploadFile;
        }


        public async Task<ListArticle> GetAll(int pageid, int take, string filter)
        {

            var Result = _articleRepo.Get.Select(c => new ArticleS()
            {
                Id = c.Id,
                Title = c.Title,
                Image = c.Image,
                Content = c.Content,
                IsActive = c.IsActive,
                ArticleCategoryId = c.ArticleCategoryId,
                ArticleCategoryName = c.ArticleCategory.Title

            });

            if (!string.IsNullOrWhiteSpace(filter))
                Result = Result.Where(c => c.Title.Contains(filter));


            var skip = (pageid - 1) * take;

            var model = new ListArticle()
            {
                Fillter = filter,
                ArticleS = await Result.OrderByDescending(c => c.Title).Skip(skip).Take(take).ToListAsync(),
            };

            model.GenaratPaging(Result, pageid, take);
            return model;

        }


        public async Task<OperationResult> Create(CreateArticle createArticle)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var Article = new TArticle()
                {
                    Title = createArticle.Title,
                    Content = createArticle.Content,
                    CreateDate = DateTime.Now,
                    ArticleCategoryId = createArticle.ArticleCategoryId,
                    IsActive = false
                };

                if (createArticle.ImagFile != null)
                    Article.Image = _uploadFile.Upload(createArticle.ImagFile);

                await _articleRepo.UpdateAsync(Article);

                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }
    }
}