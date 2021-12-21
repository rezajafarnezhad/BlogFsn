using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Application;
using Fsn.Application.Contracts.Article;
using Fsn.Domain.Blog;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepo _articleCategoryRepo;

        public ArticleCategoryApplication(IArticleCategoryRepo articleCategoryRepo)
        {
            _articleCategoryRepo = articleCategoryRepo;
        }

        public async Task<OperationResult> Create(CreateArticleCategory category)
        {

            OperationResult operationResult = new OperationResult();

            try
            {
                if (string.IsNullOrWhiteSpace(category.Title))
                    return operationResult.Failed("نام دسته وارد شود");

                if (await _articleCategoryRepo.GetNoTraking.AnyAsync(c => c.Title == category.Title))
                    return operationResult.Failed("نام تکراری میباشد");

                var ArticleCategory = new TArticleCategory()
                {
                    Title = category.Title,
                    IsActive = true,
                };

                await _articleCategoryRepo.CreateAsync(ArticleCategory);
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<ListArticleCategory> GetAll(int pageid, int take)
        {
            var Result = _articleCategoryRepo.Get.Select(c => new ArticleCategory()
            {
                Id = c.Id,
                Title = c.Title,
                IsActive = c.IsActive
            });

            var skip = (pageid - 1) * take;

            var model = new ListArticleCategory()
            {
                ArticleCategories = await Result.OrderByDescending(c => c.Title).Skip(skip).Take(take).ToListAsync()
            };

            model.GenaratPaging(Result, pageid, take);
            return model;
        }

        public async Task<CategoryForEdit> GetForEdit(Guid? Id)
        {
            var Category = await _articleCategoryRepo.Get.Where(c => c.Id == Id).Select(c => new CategoryForEdit()
            {
                Id = c.Id,
                Title = c.Title,
                IsActive = c.IsActive

            }).SingleOrDefaultAsync();

            return Category;
        }

       
        public async Task<OperationResult> Edit(CategoryForEdit categoryForEdit)
        {
            OperationResult operationResult = new OperationResult();

            if (await _articleCategoryRepo.Get.AnyAsync(c => c.Title == categoryForEdit.Title && c.Id != categoryForEdit.Id))
                return operationResult.Failed("نام تکراری میباشد");

            var ArticleCategory = new TArticleCategory() { Id = categoryForEdit.Id,Title = categoryForEdit.Title,IsActive=categoryForEdit.IsActive };
            await _articleCategoryRepo.UpdateAsync(ArticleCategory);
            return operationResult.Succeeded();
        }

        public async Task<OperationResult> Delete(Guid Id)
        {
            OperationResult operationResult = new OperationResult();

            var Category = await _articleCategoryRepo.GetById(Id);
            if (Category is null)
                return operationResult.Failed();
            await _articleCategoryRepo.DeleteAsync(Category);
            return operationResult.Succeeded();

        }

        public async Task<OperationResult> ChangeStatuse(Guid Id)
        {
            OperationResult operationResult = new OperationResult();

            var Category = await _articleCategoryRepo.GetById(Id);
            if (Category is null)
                return operationResult.Failed();

            Category.IsActive = !Category.IsActive;
            await _articleCategoryRepo.UpdateAsync(Category);
            return operationResult.Succeeded();

        }

    }
}
