using Api.Domain.AcademicSessions;
using Api.Domain.AcademicSessionTemplates;
using Api.Domain.AcademicClasses;
using Api.Domain.Countries;
using Api.Domain.Employees;
using Api.Domain.Institutions;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Entities;

public class MySchoolContext : DbContext
{
    public MySchoolContext(DbContextOptions<MySchoolContext> options)
          : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicSession>()
            .HasMany(x => x.AcademicClasses)
            .WithOne(y => y.AcademicSession)
            .HasForeignKey(y => y.AcademicSessionId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Country>()
            .HasMany(x => x.Institutions)
            .WithOne(y => y.Country)
            .HasForeignKey(y => y.CountryId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Country>()
            .HasMany(x => x.Employees)
            .WithOne(y => y.Country)
            .HasForeignKey(y => y.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Employee>()
           .HasMany(x => x.AcademicClasses)
           .WithOne(y => y.Teacher)
           .HasForeignKey(y => y.TeacherId)
           .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Institution>()
            .HasMany(x => x.AcademicSessionTemplates)
            .WithOne(y => y.Institution)
            .HasForeignKey(y => y.InstitutionId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Institution>()
            .HasMany(x => x.AcademicSessions)
            .WithOne(y => y.Institution)
            .HasForeignKey(y => y.InstitutionId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Institution>()
            .HasMany(x => x.AcademicClasses)
            .WithOne(y => y.Institution)
            .HasForeignKey(y => y.InstitutionId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Institution>()
            .HasMany(x => x.Employees)
            .WithOne(y => y.Institution)
            .HasForeignKey(y => y.InstitutionId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Institution> Institutions { get; set; }
    public DbSet<AcademicSessionTemplate> AcademicSessionTemplates { get; set; }
    public DbSet<AcademicSession> AcademicSessions { get; set; }
    public DbSet<AcademicClass> AcademicClasses { get; set; }
}
