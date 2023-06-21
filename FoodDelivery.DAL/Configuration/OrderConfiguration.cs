using FoodDelivery.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DAL.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(o => o.Basket)
            .WithMany(b => b.Orders)
            .HasForeignKey(o => o.BasketId);

            builder.Property(x => x.DateCreate)
                .HasConversion(x => x.ToString("f"), x => DateTime.ParseExact(x, "f", null));
        }
    }
}
