using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fsn.Domain.Account.AccessLevel;
using Fsn.Domain.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fsn.Infrastructure.EF.Mapping
{
    public class TCommentMapping : IEntityTypeConfiguration<TComment>
    {
        public void Configure(EntityTypeBuilder<TComment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Message).IsRequired().HasMaxLength(2000);

            builder.HasOne(c => c.Article)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
