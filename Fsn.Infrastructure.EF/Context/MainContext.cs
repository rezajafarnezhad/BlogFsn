using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fsn.Domain.Account.AccessLevel;
using Fsn.Domain.Account.RoleAgg;
using Fsn.Domain.Account.UserAgg;
using Fsn.Domain.Blog;
using Fsn.Infrastructure.EF.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fsn.Infrastructure.EF.Context
{
    public class MainContext : IdentityDbContext<TUser, TRole, Guid>
    {

        public MainContext(DbContextOptions<MainContext> op) : base(op)
        {
        }

        public MainContext()
        {

        }

        public DbSet<TUser> TUser { get; set; }
        public DbSet<TRole> TRole { get; set; }
        public DbSet<TAccessLevel> TAccessLevel { get; set; }
        public DbSet<TAccessLevelRole> TAccessLevelRole { get; set; }
        public DbSet<TArticle> TArticle  { get; set; }
        public DbSet<TArticleCategory> TArticleCategory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var asb = typeof(TUserMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(asb);

        }
    }
}
