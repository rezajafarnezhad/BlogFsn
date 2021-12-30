using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fsn.Domain.Account.RoleAgg;
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
}
