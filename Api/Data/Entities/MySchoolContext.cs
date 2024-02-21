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
        modelBuilder.Entity<AcademicSessionTemplate>()
            .HasOne(e => e.Institutions)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AcademicSession>()
            .HasOne(e => e.Institutions)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AcademicClass>()
            .HasOne(e => e.Institutions)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AcademicClass>()
            .HasOne(e => e.AcademicSessions)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AcademicClass>()
            .HasOne(e => e.Teachers)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Countries)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Institutions)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Institution>()
            .HasOne(e => e.Countries)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Institution> Institutions { get; set; }
    public DbSet<AcademicSessionTemplate> AcademicSessionTemplates { get; set; }
    public DbSet<AcademicSession> AcademicSessions { get; set; }
    public DbSet<AcademicClass> AcademicClasses { get; set; }
}
