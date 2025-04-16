// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly AppDbContext _context;

    public AuthController(IAuthService authService, AppDbContext context)
    {
        _authService = authService;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (_context.Users.Any(u => u.Email == registerDto.Email))
            return BadRequest("El usuario ya existe");

        var user = new User
        {
            Email = registerDto.Email,
            PasswordHash = _authService.HashPassword(registerDto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("Usuario registrado exitosamente");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto loginDto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);
        if (user == null) return Unauthorized();

        if (!_authService.VerifyPassword(loginDto.Password, user.PasswordHash))
            return Unauthorized();

        var token = _authService.GenerateToken(user);
        return Ok(new { Token = token });
    }
}