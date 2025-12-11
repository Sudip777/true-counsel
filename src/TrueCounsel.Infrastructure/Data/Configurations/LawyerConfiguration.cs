using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Infrastructure.Data.Configurations
{
    public class LawyerConfiguration : IEntityTypeConfiguration<Lawyer>
    {
        public void Configure(EntityTypeBuilder<Lawyer> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            builder.Property(x => x.HourlyRate)
                   .HasPrecision(18, 2); // Fix EF decimal warning


        }
    }
}