using Api.Data.Entities;
using Api.Domain.Employees;
using Api.Domain.Institutions;

namespace Api.Domain.Countries;

public class Country : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Iso2Code { get; set; } = string.Empty;
    public virtual ICollection<Institution> Institutions { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}
