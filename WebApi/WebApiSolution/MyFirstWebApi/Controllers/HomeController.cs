using Microsoft.AspNetCore.Mvc;
using MyFirstWebApi.Conctext;
using MyFirstWebApi.Entities;

namespace MyFirstWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class HomeController : ControllerBase
{

    
    //AppDbContext _context;

    //public HomeController(AppDbContext context)
    //{
    //    _context = context;
    //}

    [HttpGet("{id}/{age}")]
    public IActionResult GetAll(int id, int age)
    {
        //ExampleDbContext context = new();

        AppDbContext context = new();

        context.Set<Todo>();
        return Ok(new { Message = "Api çalışıyor!" });
    }

    [HttpPost]
    public IActionResult Create(User user) 
    {
        return Ok(new { Message = "Kayıt oluşturuldu" });
    }
}
