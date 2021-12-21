using Fsn.Domain.Account.AccessLevel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fsn.Infrastructure.EF.Mapping
{
    public class TAccessLevelMapping : IEntityTypeConfiguration<TAccessLevel>
    {
        public void Configure(EntityTypeBuilder<TAccessLevel> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(300);

            builder.HasMany(c => c.TUsers)
                .WithOne(c => c.TAccessLevel)
                .HasForeignKey(c => c.AccessLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.TAccessLevelRoles)
                .WithOne(c => c.TAccessLevel)
                .HasForeignKey(c => c.AccessLevelId);

        }
    }
}