using FoodDelivery.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.DAL.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Address)
                .HasMaxLength(100);
            builder.Property(x => x.Commentary)
                .HasMaxLength(200);
            builder.Property(x => x.Price)
                .HasColumnType("decimal(7,2)");
            builder.HasOne(o => o.Basket)
            .WithMany(b => b.Orders)
            .HasForeignKey(o => o.BasketId);
            builder.Property(x => x.DateCreate)
                .HasConversion(x => x.ToString("G"), x => DateTime.ParseExact(x, "G", null));
        }
    }
}
