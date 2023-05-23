namespace ProjectManager.Models;

public class Project : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid? OwnerId { get; set; }
    public User? Owner { get; set; }
}
