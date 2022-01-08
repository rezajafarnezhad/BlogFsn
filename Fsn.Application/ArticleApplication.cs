using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application;
using Fsn.Application.Contracts.Article;
using Fsn.Application.Interfaces;
using Fsn.Domain.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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


        public async Task<ListArticle> GetAll(int pageid, int take, string filter,string CategorySearch)
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

            if (CategorySearch is "All")
                CategorySearch = null;

            if (!string.IsNullOrWhiteSpace(CategorySearch))
            {
                Result = Result.Where(c => c.ArticleCategoryName == CategorySearch);
            }

            var skip = (pageid - 1) * take;

            var model = new ListArticle()
            {
                Fillter = filter,
                CategorySearch = CategorySearch,
                ArticleS = await Result.OrderByDescending(c => c.Title).Skip(skip).Take(take).ToListAsync(),
            };

            model.GenaratPaging(Result, pageid, take);
            return model;

        }
        
        public async Task<ListArticle> GetAllInIndexPage(int pageid, int take, string filter)
        {

            var Result = _articleRepo.Get.Where(c=>c.IsActive).Select(c => new ArticleS()
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
                CategorySearch = null,
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

                if(await _articleRepo.Get.AnyAsync(c=>c.Title == Article.Title))
                    return operationResult.Failed("عنوان تکراری میباشد");

                await _articleRepo.CreateAsync(Article);

                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<Edit> GetForEdit(Guid Id)
        {
            var Article = await _articleRepo.Get.Where(c => c.Id == Id)
                .Select(c => new Edit()
                {
                    Id = c.Id,
                    Image = c.Image,
                    Title = c.Title,
                    ArticleCategoryId = c.ArticleCategoryId,
                    //CreationDate = c.CreateDate.ToString(),
                    Content = c.Content,
                }).SingleOrDefaultAsync();

            if (Article == null)
                return null;

            return Article;

        }

        public async Task<OperationResult> Edit(Edit edit)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var Article = await _articleRepo.GetById(edit.Id);
                if (Article is null)
                    return operationResult.Failed();

                Article.Title = edit.Title;
                Article.Content = edit.Content;
                Article.ArticleCategoryId = edit.ArticleCategoryId;
                if (edit.ImagFile != null)
                {
                    _uploadFile.DeleteFile(Article.Image);
                    Article.Image = _uploadFile.Upload(edit.ImagFile);
                }

                await _articleRepo.UpdateAsync(Article);
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }



        public async Task<OperationResult> Delete(Guid Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {

                var Article = await _articleRepo.GetById(Id);
                if (Article is null)
                    return operationResult.Failed();

                await _articleRepo.DeleteAsync(Article);
                return operationResult.Succeeded();


            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }


        public async Task<OperationResult> ChangeStatus(Guid Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {

                var Article = await _articleRepo.GetById(Id);
                if (Article is null)
                    return operationResult.Failed();

                Article.IsActive = !Article.IsActive;
                await _articleRepo.UpdateAsync(Article);
                return operationResult.Succeeded();


            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<ListArticle> GetArticleByCategoryTitle(int pageid, int take, string Title)
        {
            try
            {
                var Result = _articleRepo.Get.Where(c => c.ArticleCategory.Title == Title && c.IsActive).Select(c => new ArticleS()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Content = c.Content,
                    Image = c.Image,
                    ArticleCategoryId = c.ArticleCategoryId,
                    ArticleCategoryName = c.Title,
                });


                var skip = (pageid - 1) * take;

                var model = new ListArticle()
                {
                    Fillter = null,
                    CategorySearch = Title,
                    ArticleS = await Result.OrderByDescending(c => c.Title).Skip(skip).Take(take).ToListAsync(),
                };
                model.GenaratPaging(Result, pageid, take);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<LastArticle>> GetForLastArticle()
        {
            try
            {
                var _LastArticle = await _articleRepo.Get.Where(c => c.IsActive).OrderByDescending(c=>c.CreateDate).Take(4).Select(c => new LastArticle()
                {
                    Id = c.Id.ToString(),
                    Title = c.Title,
                    Content = c.Content.Substring(0, Math.Min(c.Content.Length, 130)) + "....",
                    CommentCount = 12,
                    Image = c.Image

                }).ToListAsync();

                return _LastArticle;
            }
            catch (Exception )
            {
                return null;
            }
        }

        public async Task<SingleArticle> GetForSinglePage(string Id)
        {
            try
            {
                var qdata = await _articleRepo.Get.Where(c => c.Id == Guid.Parse(Id)).Select(c => new SingleArticle()
                {
                    Id = c.Id.ToString(),
                    Content = c.Content,
                    Image = c.Image,
                    CommentCount = 15,
                    Title = c.Title
                }).SingleOrDefaultAsync();

                return qdata;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}