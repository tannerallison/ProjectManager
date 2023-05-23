using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectManager.Helpers;
using ProjectManager.Models;

namespace ProjectManager.Services;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User? GetById(Guid id);
    (string, AuthenticateResponse) Register(AuthenticateRequest model);
}

/// <summary>
/// Example pulled from https://jasonwatmore.com/post/2021/12/14/net-6-jwt-authentication-tutorial-with-example-api#user-service-cs
/// </summary>
public class UserService : IUserService
{
    private readonly AppSettings _appSettings;
    private readonly AppDbContext _context;

    public UserService(IOptions<AppSettings> appSettings, AppDbContext context)
    {
        _appSettings = appSettings.Value;
        _context = context;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);
        if (user == null) return null;

        var passwordHash = new PasswordHash(user.PasswordHash);
        if (!passwordHash.Verify(model.Password))
            return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public User? GetById(Guid id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }

    /// <summary>
    /// Registers a new user with the given credentials.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public (string, AuthenticateResponse) Register(AuthenticateRequest model)
    {
        if (_context.Users.Any(x => x.Username == model.Username))
            return ("Username \"" + model.Username + "\" is already taken", null)!;

        var passwordHash = new PasswordHash(model.Password);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = model.Username,
            PasswordHash = passwordHash.ToArray()
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        var authenticateResponse = Authenticate(model);
        return (null, authenticateResponse)!;
    }

    // helper methods

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
