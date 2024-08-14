using Assignment.Domain.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Database.Configuration
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(x => x.Reviews).HasForeignKey(x => x.UserId).HasConstraintName("FK_User_Review");
            builder.HasOne(x => x.Product).WithMany(x => x.Reviews).HasForeignKey(x => x.ProductId).HasConstraintName("FK_Product_Review");
        }
    }
}
