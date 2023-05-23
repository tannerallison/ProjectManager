using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Models;

public abstract class BaseEntity : ITimeStamped
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
