using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrueCounsel.Domain.Entities;


namespace TrueCounsel.Infrastructure.Data.Configurations
{
    public class LegalCaseConfiguration : IEntityTypeConfiguration<LegalCase>
    {
        public void Configure(EntityTypeBuilder<LegalCase> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            // CaseType relationship (required)
            builder.HasOne(lc => lc.CaseType)
                   .WithMany()
                   .HasForeignKey(lc => lc.CaseTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Court relationship (optional)
            builder.HasOne(lc => lc.Court)
                   .WithMany()
                   .HasForeignKey(lc => lc.CourtId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Client and Lawyer relationships (required)
            builder.HasOne(lc => lc.Client)
                   .WithMany()
                   .HasForeignKey(lc => lc.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(lc => lc.Lawyer)
                   .WithMany()
                   .HasForeignKey(lc => lc.LawyerId)
                   .OnDelete(DeleteBehavior.Restrict);

            // CaseCategory (optional)
            builder.HasOne(lc => lc.CaseCategory)
                   .WithMany()
                   .HasForeignKey(lc => lc.CaseCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Decimal property precision
            builder.Property(lc => lc.EstimatedValue)
                   .HasPrecision(18, 2);
        }
    }
}

