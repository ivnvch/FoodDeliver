using FoodDelivery.DAL.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Description)
                .HasMaxLength(200);
            builder.Property(x => x.CreationDate)
           .HasConversion(x => x.ToString("G"), x => DateTime.ParseExact(x, "G", null));

        }
    }
}
