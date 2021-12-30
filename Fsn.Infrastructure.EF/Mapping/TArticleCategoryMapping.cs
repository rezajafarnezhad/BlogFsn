using Fsn.Domain.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fsn.Infrastructure.EF.Mapping
{
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