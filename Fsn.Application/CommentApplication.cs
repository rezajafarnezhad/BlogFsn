using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application;
using Framework.Infrastructure;
using Fsn.Application.Contracts.Article;
using Fsn.Application.Contracts.Commnet;
using Fsn.Application.Interfaces;
using Fsn.Domain.Blog;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepo _commentRepo;
        public CommentApplication(ICommentRepo commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public async Task<OperationResult> CreateCommnet(CreateCommnet comment)
        {
            OperationResult operationResult = new OperationResult();

            try
            {

                var _Comment = new TComment()
                {
                    Id = new Guid().SequentialGuid(),
                    UserId = Guid.Parse(comment.UserId),
                    Message = comment.Message,
                    Status = Convert.ToInt32(StatusComment.New),
                    CreateionDate = DateTime.Now,
                    ArticleId = Guid.Parse(comment.ArticleId),
                };

                await _commentRepo.CreateAsync(_Comment);
                return operationResult.Succeeded("کامنت شما با موفقیت درج شد");
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<ListComment> GetAll(int pageId, int take, string filter)
        {

            try
            {
                var result = _commentRepo.Get.Include(c => c.User).Select(c => new CommnetModel()
                {
                    Id = c.Id.ToString(),
                    ArticleId = c.ArticleId.ToString(),
                    UserId = c.UserId.ToString(),
                    Message = c.Message,
                    Date = c.CreateionDate.ToFarsi(),
                    Status = c.Status,
                    FullName = c.User.FullName,
                    ArticleTitle = c.Article.Title,
                    Email = c.User.Email,
                });

                if (!string.IsNullOrWhiteSpace(filter))
                    result = result.Where(c => c.ArticleTitle.Contains(filter));

                var skip = (pageId - 1) * take;

                var model = new ListComment()
                {
                    Filter = filter,
                    CommnetModels = await result.OrderBy(c => c.Status).Skip(skip).Take(take).ToListAsync(),
                };

                model.GenaratPaging(result, pageId, take);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CommnetModel> Show(string Id)
        {
            try
            {
                var _comment = await _commentRepo.Get.Where(c => c.Id == Guid.Parse(Id)).Select(c => new CommnetModel()
                {
                    Message = c.Message,
                    ArticleTitle = c.Article.Title,
                    FullName = c.User.FullName,
                    Email = c.User.Email,
                    Date = c.CreateionDate.ToFarsi(),
                    Status = c.Status,
                })
                    .SingleOrDefaultAsync();
                return _comment;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<CommnetModel>> ShowByArticleId(string Id)
        {
            try
            {
                var _comment = await _commentRepo.Get.Where(c => c.ArticleId == Guid.Parse(Id)).Select(c => new CommnetModel()
                    {
                        Message = c.Message,
                        ArticleTitle = c.Article.Title,
                        FullName = c.User.FullName,
                        Email = c.User.Email,
                        Date = c.CreateionDate.ToFarsi(),
                        Status = c.Status,
                    })
                    .ToListAsync();
                return _comment;
            }
            catch (Exception)
            {
                return null;
            }
        }



        public async Task<OperationResult> Confirm(string Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _comment = await _commentRepo.GetById(Guid.Parse(Id));
                if (_comment is null)
                    return operationResult.Failed();

                _comment.Status = Convert.ToInt32(StatusComment.Confirmed);

                await _commentRepo.UpdateAsync(_comment);
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<OperationResult> NotConfirm(string Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _comment = await _commentRepo.GetById(Guid.Parse(Id));
                if (_comment is null)
                    return operationResult.Failed();

                _comment.Status = Convert.ToInt32(StatusComment.Cancel);

                await _commentRepo.UpdateAsync(_comment);
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<OperationResult> Delete(string Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _comment = await _commentRepo.GetById(Guid.Parse(Id));
                if (_comment is null)
                    return operationResult.Failed();

                await _commentRepo.DeleteAsync(_comment);
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }
    }
}