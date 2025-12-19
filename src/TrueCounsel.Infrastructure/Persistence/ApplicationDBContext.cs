// File: src/TrueCounsel.Infrastructure/Data/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
using System;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

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

       
    }
}