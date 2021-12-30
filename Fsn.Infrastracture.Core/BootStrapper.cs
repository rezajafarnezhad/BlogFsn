using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Domain;
using Framework.Infrastructure;
using Fsn.Application;
using Fsn.Application.Contracts.AccessLevel;
using Fsn.Application.Contracts.Article;
using Fsn.Application.Contracts.User;
using Fsn.Application.Interfaces;
using Fsn.Domain.Account.AccessLevel;
using Fsn.Domain.Account.RoleAgg;
using Fsn.Domain.Account.UserAgg;
using Fsn.Domain.Blog;
using Fsn.Infrastructure.EF.Context;
using Fsn.Infrastructure.EF.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fsn.Infrastracture.Core
{
    public static class BootStrapper
    {

        public static void Config(this IServiceCollection service, string connectionString)
        {

            service.AddDbContext<MainContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

            service.AddSingleton(typeof(IRepo<>), typeof(BaseRepo<>));

            service.AddScoped<IUserRepo, UserRepo>();
            service.AddScoped<IUserApplication, UserApplication>();

            service.AddScoped<IRoleRepo, RoleRepo>();
            service.AddScoped<IRoleApplication, RoleApplication>();

            service.AddScoped<IAccessLevelRepo, AccessLevelRepo>();
            service.AddScoped<IAccessLevelApplication, AccessLevelApplication>();

            service.AddScoped<IAccessLevelRoleRepo, AccessLevelRoleRepo>();
            service.AddScoped<IAccessLevelRoleApplication, AccessLevelRoleApplication>();

            service.AddScoped<IArticleRepo, ArticleRepo>();
            service.AddScoped<IArticleApplication, ArticleApplication>();

            service.AddScoped<IArticleCategoryRepo, ArticleCategoryRepo>();
            service.AddScoped<IArticleCategoryApplication, ArticleCategoryApplication>();
        }
    }
}
