using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fsn.Domain.Account.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fsn.Infrastructure.EF.Mapping
{
    public class TUserMapping:IEntityTypeConfiguration<TUser>
    {
        public void Configure(EntityTypeBuilder<TUser> builder)
        {
            //mapping
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FullName).IsRequired().HasMaxLength(50);

            builder.HasOne(c => c.TAccessLevel)
                .WithMany(c => c.TUsers)
                .HasForeignKey(c => c.AccessLevelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
