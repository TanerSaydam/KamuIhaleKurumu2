using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyFirstWebApi.Conctext;
using MyFirstWebApi.Controllers;
using MyFirstWebApi.Entities;
using MyFirstWebApi.IoC;
using MyFirstWebApi.Options;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyFirstWebApi.Authentication
{
    public static class JwtProvider
    {
        public static LoginResponse CreateToken(Entities.User user, List<Role> roles)
        {
            #region MyRegion
            roles = new()
            {
                new Role(){ Id = "", Name = "Menu.Home"},
                new Role(){ Id = "", Name = "Menu.About"},
                new Role(){ Id = "", Name = "Menu.About"},
            };

            var claims = new Claim[]
            {
                new Claim("UserId",user.Id),
                new Claim(ClaimTypes.Role,JsonConvert.SerializeObject(roles))
            };
            #endregion




            var jwt = ServiceTool.ServiceProvider.GetRequiredService<IOptions<Jwt>>();

            DateTime expires = DateTime.Now.AddSeconds(10);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Value.SecretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: jwt.Value.Issuer,
                audience: jwt.Value.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: signingCredentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            user.RefreshToken = Guid.NewGuid().ToString();
            user.RefreshTokenExpires = expires.AddHours(1);

            AppDbContext context = new();
            context.Set<Entities.User>().Update(user);
            context.SaveChanges();

            return new(token,user.Id,roles, user.RefreshToken, user.RefreshTokenExpires);
        }
    }
}
