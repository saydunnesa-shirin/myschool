namespace Api.Data.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public int CreatedBy { get; set; }
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    public int UpdatedBy { get; set; }
    public bool IsActive { get; set; } = true;
}
