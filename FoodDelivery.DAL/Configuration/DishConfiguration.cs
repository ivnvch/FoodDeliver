using FoodDelivery.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.DAL.Configuration
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dishes").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(20);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(7,2)");

            builder.Property(x => x.Weight)
                .HasColumnType("decimal(5,3)");
        }
    }
}
