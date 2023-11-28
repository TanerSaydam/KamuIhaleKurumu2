using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ValuesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var response = _context.Categories.ToList();
        stopwatch.Stop();

        return Ok(stopwatch.ElapsedMilliseconds);
    }
}