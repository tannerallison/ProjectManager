using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectManager.Helpers;
using ProjectManager.Models;
using ProjectManager.Services;

namespace ProjectManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] AuthenticateRequest model)
    {
        (var error, var user) = _userService.Register(model);

        if (!error.IsNullOrEmpty())
            return BadRequest(new { message = error });

        return Ok(user);
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] AuthenticateRequest model)
    {
        var user = _userService.Authenticate(model);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(user);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
}
