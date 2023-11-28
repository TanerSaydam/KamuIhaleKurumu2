using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApi.Authentication;
using MyFirstWebApi.Conctext;
using MyFirstWebApi.Entities;

namespace MyFirstWebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class AuthController : ControllerBase
{
    private AppDbContext context = new();

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto request, CancellationToken cancellationToken)
    {
        Entities.User user = 
            await context.Set<Entities.User>()
            .Where(p=> p.UserName == request.UserName)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null) throw new Exception("Kullanıcı bulunamadı!");

        LoginResponse response = JwtProvider.CreateToken(user, new List<Role>());
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> LoginWithProvide(LoginProvideDto request, CancellationToken cancellationToken)
    {
        Entities.User user = 
            await context.Set<Entities.User>()
            .FirstOrDefaultAsync(p => p.UserName == request.UserName, cancellationToken);

        if(user == null)
            return BadRequest(new { Message = "Kullanıcı bulunamadı!" });

        var response = new ProvideResponseDto(user.Provider, user.UserName);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTokenByRefreshToken(CreateTokenDto request, CancellationToken cancellationToken)
    {
        Entities.User user = await context.Set<Entities.User>().Where(p=> p.RefreshToken == request.RefreshToken).FirstOrDefaultAsync(cancellationToken);

        if (user == null) return BadRequest("Refresh token geçersiz");

        if(user.RefreshTokenExpires < DateTime.Now) return BadRequest("Refresh token geçersiz");

        var response = JwtProvider.CreateToken(user, new List<Role>());
        return Ok(response);
    }

    [HttpGet]
    public IActionResult SetRoles()
    {
        List<Role> roles = new List<Role>();
        roles.Add(new Role() { Name = "Menu.Home" });
        roles.Add(new Role() { Name = "Menu.About" });
        roles.Add(new Role() { Name = "Menu.Contact" });
        roles.Add(new Role() { Name = "TodoPost.Create" });
        roles.Add(new Role() { Name = "TodoPost.Update" });
        roles.Add(new Role() { Name = "TodoPost.Delete" });
        roles.Add(new Role() { Name = "TodoPost.GetAll" });

        context.Set<Role>().AddRange(roles);

        List<UserRole> userRoless = new List<UserRole>();
        User user = context.Set<User>().FirstOrDefault();
        foreach (var res in roles)
        {
            UserRole userRole = new()
            {
                UserId = user.Id,
                RoleId = res.Id
            };

            userRoless.Add(userRole);
        } 

        context.Set<UserRole>().AddRange(userRoless);

        context.SaveChanges();
        return NoContent();

    }
}

public sealed record CreateTokenDto(
    string RefreshToken);
public sealed record ProvideResponseDto(
    string Provide,
    string UserName);
public sealed record LoginProvideDto(
    string UserName);

public sealed record LoginDto(
    string UserName);
