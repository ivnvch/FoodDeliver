using FoodDelivery.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.DAL.Configuration
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profiles").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName).HasMaxLength(25);
            builder.Property(x => x.LastName).HasMaxLength(50);
            builder.Property(x => x.MiddleName).HasMaxLength(50);

            builder.Property(x => x.DateCreated)
                .HasConversion(x => x.ToString("G"), x => DateTime.ParseExact(x, "G", null));
        }
    }
}
  