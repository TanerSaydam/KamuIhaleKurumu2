using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApi.Conctext;
using MyFirstWebApi.Entities;
using System.Security.Claims;

namespace MyFirstWebApi.Attributes;

public sealed class RoleAttribute : Attribute, IAuthorizationFilter
{
    private string _role;

    public RoleAttribute(string role)
    {
        _role = role;
    }

    public RoleAttribute()
    {
        
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (string.IsNullOrEmpty(_role))
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
            {
                string controllerName = descriptor.ControllerName;
                string actionName = descriptor.ActionName;
                _role = $"{controllerName}.{actionName}";
            }
        }

        var userId = context.HttpContext.User.FindFirstValue("UserId");

        if (userId == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        AppDbContext dbContext = new(); 

        List<Role> roles =
            dbContext.Set<UserRole>()
            .Where(p => p.UserId == userId)
            .Select(p => p.Role)
            .ToList();

        if(!roles.Any(p=> p.Name == _role))
        {
            context.Result = new UnauthorizedResult();
            context.HttpContext.Response.StatusCode = 403;
            return;
        }
    }
}
