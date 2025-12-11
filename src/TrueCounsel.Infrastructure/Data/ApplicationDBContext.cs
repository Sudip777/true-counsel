// File: src/TrueCounsel.Infrastructure/Data/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets — all correct now
    public DbSet<User> Users => Set<User>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Lawyer> Lawyers => Set<Lawyer>();
    public DbSet<LegalCase> LegalCases => Set<LegalCase>();                 
    public DbSet<CaseType> CaseTypes => Set<CaseType>();
    public DbSet<CaseCategory> CaseCategories => Set<CaseCategory>();
    public DbSet<Court> Courts => Set<Court>();
    public DbSet<CaseNote> CaseNotes => Set<CaseNote>();
    public DbSet<CaseParty> CaseParties => Set<CaseParty>();
    public DbSet<CaseStatusHistory> CaseStatusHistories => Set<CaseStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This line fixes CA1062 warning — modelBuilder is never null in EF Core
        ArgumentNullException.ThrowIfNull(modelBuilder);
        // Best practice: load all IEntityTypeConfiguration from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // === Value Object Mappings (Owned Types) ===
        modelBuilder.Entity<LegalCase>(b =>
        {
            b.ToTable("LegalCases");

            // Collections use backing fields (your domain uses private List<T>)
            b.HasMany(c => c.Parties)
             .WithOne(p => p.LegalCase)
             .HasForeignKey(p => p.CaseId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(c => c.Notes)
             .WithOne(n => n.LegalCase)
             .HasForeignKey(n => n.CaseId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(c => c.StatusHistory)
             .WithOne(sh => sh.LegalCase)
             .HasForeignKey(sh => sh.CaseId)
             .OnDelete(DeleteBehavior.Cascade);

            // IMPORTANT: set Restrict/NoAction to avoid multiple cascade paths
            b.HasOne(x => x.Client)
                .WithMany(c => c.Cases!)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict); // <- NO CASCADE

            b.HasOne(x => x.Lawyer)
                .WithMany(l => l.AssignedCases)
                .HasForeignKey(x => x.LawyerId)
                .OnDelete(DeleteBehavior.Restrict); // <- NO CASCADE

            b.HasOne(x => x.CaseType)
                .WithMany()
                .HasForeignKey(x => x.CaseTypeId)
                .OnDelete(DeleteBehavior.Restrict); // <- NO CASCADE

            b.HasOne(x => x.CaseCategory)
                .WithMany(cc => cc.Cases!)
                .HasForeignKey(x => x.CaseCategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Court)
                .WithMany()
                .HasForeignKey(x => x.CourtId)
                .OnDelete(DeleteBehavior.SetNull);

            // ... other mappings ...
        });

        // ensure Lawyer navigation uses field access
        modelBuilder.Entity<Lawyer>(b =>
        {
            b.ToTable("Lawyers");
            b.Navigation(l => l.AssignedCases).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        // ... other entity configs ...
    }
}