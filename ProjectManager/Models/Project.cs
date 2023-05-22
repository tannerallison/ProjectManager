namespace ProjectManager.Models;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid? OwnerId { get; set; }
    public User? Owner { get; set; }
}
