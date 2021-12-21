using Fsn.Domain.Account.AccessLevel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fsn.Infrastructure.EF.Mapping
{
    public class TAccessLevelRoleMapping : IEntityTypeConfiguration<TAccessLevelRole>
    {
        public void Configure(EntityTypeBuilder<TAccessLevelRole> builder)
        {
            
            builder.HasKey(c => c.Id);


            builder.HasOne(c => c.TRole)
                .WithMany(c => c.TAccessLevelRoles)
                .HasForeignKey(c => c.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.TAccessLevel)
                .WithMany(c => c.TAccessLevelRoles)
                .HasForeignKey(c => c.AccessLevelId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}