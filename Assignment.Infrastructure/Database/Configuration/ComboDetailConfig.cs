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
    public class ComboDetailConfig : IEntityTypeConfiguration<ComboDetail>
    {
        public void Configure(EntityTypeBuilder<ComboDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Combo).WithMany(x => x.ComboDetails).HasForeignKey(x => x.ComboId).HasConstraintName("FK_Combo_ComboDetail");
            builder.HasOne(x => x.Product).WithMany(x => x.ComboDetails).HasForeignKey(x => x.ProductId).HasConstraintName("FK_Product_ComboDetail");
        }
    }
}
