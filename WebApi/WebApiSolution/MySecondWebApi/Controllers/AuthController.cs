using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MySecondWebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost]
    public IActionResult LoginWithProvider(LoginProvideDto request)
    {
        //aldığım provide doğrula


        //context.Users.Where(p=> p.Provide == request.Provide).FirstOrDefault();
        //if(user == null) { //User Create }
        //strig token = CreateToken(user);
        //return Ok(token)
        return Ok(new LoginResponseDto());
    }
}

public record LoginResponseDto(
    string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

public record LoginProvideDto(
    string Provide,
    string UserName);