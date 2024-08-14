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
    public class ComboConfig : IEntityTypeConfiguration<Combo>
    {
        public void Configure(EntityTypeBuilder<Combo> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
