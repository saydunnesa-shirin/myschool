using Api.Data.Entities;

namespace Api.Domain.Institutions
{
    public class InstitutionViewModel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string Address { get; set; }
        public string CountryName { get; set; } = string.Empty;
    }
}
