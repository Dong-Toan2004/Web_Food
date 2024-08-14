using Assignment.Domain.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Database.Configuration
{
    public class CartDetailConfig : IEntityTypeConfiguration<Cartdetail>
    {
        public void Configure(EntityTypeBuilder<Cartdetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Cart).WithMany(x => x.Cartdetail).HasForeignKey(x => x.CartId).HasConstraintName("FK_Cart_CartDetail");
            builder.HasOne(x => x.Product).WithMany(x => x.Cartdetails).HasForeignKey(x => x.ProductId).HasConstraintName("FK_Product_CartDetail");
        }
    }
}
