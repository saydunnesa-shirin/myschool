namespace Api.Data.Entities;

public class BaseEntity
{
  public int Id { get; set; }
  public DateTime CreatedDate { get; set; }
  public int CreatedBy { get; set; }
  public DateTime UpdatedDate { get; set; }
  public int UpdatedBy { get; set; }
  public bool IsActive { get; set; }
}
