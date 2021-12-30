using Fsn.Domain.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fsn.Infrastructure.EF.Mapping
{
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
}