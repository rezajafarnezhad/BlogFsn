using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fsn.Domain.Account.RoleAgg;
using Fsn.Domain.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fsn.Infrastructure.EF.Mapping
{
    public class TRoleMapping : IEntityTypeConfiguration<TRole>
    {
        public void Configure(EntityTypeBuilder<TRole> builder)
        {
         
            builder.HasKey(c => c.Id);
            builder.Property(c => c.PageName).IsRequired().HasMaxLength(300);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(700);


            builder.HasOne(c => c.TRole_Parent)
                .WithMany(c => c.TRole_Childs)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.TRole_Childs)
                .WithOne(c => c.TRole_Parent)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.TAccessLevelRoles)
                .WithOne(c => c.TRole)
                .HasForeignKey(c => c.RoleId);
        }
    }

    public class TArticleMapping : IEntityTypeConfiguration<TArticle>
    {
        public void Configure(EntityTypeBuilder<TArticle> builder)
        {

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(300);
            builder.Property(c => c.Content).IsRequired().HasMaxLength(6000);


            builder.HasOne(c => c.ArticleCategory)
                .WithMany(c => c.Articles)
                .HasForeignKey(c => c.ArticleCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class TArticleCategoryMapping : IEntityTypeConfiguration<TArticleCategory>
    {
        public void Configure(EntityTypeBuilder<TArticleCategory> builder)
        {

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(300);
            
            builder.HasMany(c => c.Articles)
                .WithOne(c => c.ArticleCategory)
                .HasForeignKey(c => c.ArticleCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
