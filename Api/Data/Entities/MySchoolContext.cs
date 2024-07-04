using Api.Domain.AcademicSessions;
using Api.Domain.AcademicClassTemplates;
using Api.Domain.AcademicClasses;
using Api.Domain.Countries;
using Api.Domain.Employees;
using Api.Domain.Institutions;
using Microsoft.EntityFrameworkCore;
using Api.Domain.Students;
using Api.Domain.Enrollments;

namespace Api.Data.Entities;

public class MySchoolContext : DbContext
{
    public MySchoolContext(DbContextOptions<MySchoolContext> options)
          : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicClass>()
            .HasMany(x => x.Students)
            .WithOne(y => y.AcademicClass)
            .HasForeignKey(y => y.ActiveClassId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<AcademicClass>()
            .HasMany(x => x.Enrollments)
            .WithOne(y => y.AcademicClass)
            .HasForeignKey(y => y.AcademicClassId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AcademicSession>()
            .HasMany(x => x.AcademicClasses)
            .WithOne(y => y.AcademicSession)
            .HasForeignKey(y => y.AcademicSessionId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<AcademicSession>()
            .HasMany(x => x.Students)
            .WithOne(y => y.AcademicSession)
            .HasForeignKey(y => y.ActiveSessionId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<AcademicSession>()
            .HasMany(x => x.Enrollments)
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
        modelBuilder.Entity<Country>()
            .HasMany(x => x.Students)
            .WithOne(y => y.Country)
            .HasForeignKey(y => y.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Employee>()
           .HasMany(x => x.AcademicClasses)
           .WithOne(y => y.Teacher)
           .HasForeignKey(y => y.TeacherId)
           .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Institution>()
            .HasMany(x => x.AcademicClassTemplates)
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
        modelBuilder.Entity<Institution>()
            .HasMany(x => x.Students)
            .WithOne(y => y.Institution)
            .HasForeignKey(y => y.InstitutionId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Institution>()
            .HasMany(x => x.Enrollments)
            .WithOne(y => y.Institution)
            .HasForeignKey(y => y.InstitutionId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AcademicClassTemplate>()
            .ToTable("AcademicClassTemplates");

        modelBuilder.Entity<AcademicClassTemplate>()
            .HasIndex(e => e.SerialNo)
            .IsUnique();
    }

    public DbSet<AcademicClass> AcademicClasses { get; set; }
    public DbSet<AcademicSession> AcademicSessions { get; set; }
    public DbSet<AcademicClassTemplate> AcademicClassTemplates { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Institution> Institutions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
}
