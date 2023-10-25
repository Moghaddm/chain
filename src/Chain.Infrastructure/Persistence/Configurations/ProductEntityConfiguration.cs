using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Domain;
using Chain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;

namespace Chain.Infrastructure.Persistence.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(x => x.Name).HasMaxLength(125);
            builder.Property(x => x.Description).HasMaxLength(300);
            builder.Property(x => x.FullEnglishName).HasMaxLength(80);
            builder.Property(x => x.SuggestPercent).HasMaxLength(101);

            builder.HasOne<Category>().WithMany().HasForeignKey("CategoryId");
            builder.HasOne<Company>().WithMany().HasForeignKey("CompanyId");

            builder.OwnsMany<Comment>(x => x.Comments);
            builder.OwnsMany(x => x.Attachments, conf => conf.ToJson());
        }
    }
}
