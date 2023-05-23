using Newtonsoft.Json;

namespace ProjectManager.Models;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordHash { get; set; } = null!;
}
