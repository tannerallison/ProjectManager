using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models;

public class AuthenticateRequest
{
    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }
}
