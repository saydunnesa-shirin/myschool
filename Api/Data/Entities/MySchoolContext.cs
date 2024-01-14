using Api.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Entities;

public class MySchoolContext : DbContext
{
  public MySchoolContext(DbContextOptions<MySchoolContext> options)
        : base(options)
  {
  }

  public DbSet<Employee> Employees { get; set; }

  //public string DbPath { get; }

  //protected override void OnConfiguring(DbContextOptionsBuilder options)
  //{
  //  options.UseSqlite($"Data Source={DbPath}");
  //}
}
