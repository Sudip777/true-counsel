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

            builder.ToTable("LegalCases");

            // Collections use backing fields (ensure LegalCase exposes IReadOnlyCollection<T> backed by a List<T>)
            builder.HasMany(c => c.Parties)
                   .WithOne(p => p.LegalCase)
                   .HasForeignKey(p => p.CaseId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(c => c.Parties).UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Notes)
                   .WithOne(n => n.LegalCase)
                   .HasForeignKey(n => n.CaseId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(c => c.Notes).UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.StatusHistory)
                   .WithOne(sh => sh.LegalCase)
                   .HasForeignKey(sh => sh.CaseId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(c => c.StatusHistory).UsePropertyAccessMode(PropertyAccessMode.Field);

            // Principal-side navigations must be referenced explicitly so EF does NOT create shadow FKs:
            builder.HasOne(c => c.Client)
                   .WithMany(cl => cl.Cases)        // must match Client.Cases
                   .HasForeignKey(c => c.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Lawyer)
                   .WithMany(l => l.AssignedCases)  // must match Lawyer.AssignedCases
                   .HasForeignKey(c => c.LawyerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.CaseType)
                   .WithMany(ct => ct.Cases)        // must match CaseType.Cases
                   .HasForeignKey(c => c.CaseTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.CaseCategory)
                   .WithMany(cc => cc.Cases)        // must match CaseCategory.Cases
                   .HasForeignKey(c => c.CaseCategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.Court)
                   .WithMany(crt => crt.Cases)      // must match Court.Cases
                   .HasForeignKey(c => c.CourtId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Decimal precision for money fields
            builder.Property(c => c.EstimatedValue).HasPrecision(18, 2);

            // Any other LegalCase-specific configuration goes here
        }
    }
}

