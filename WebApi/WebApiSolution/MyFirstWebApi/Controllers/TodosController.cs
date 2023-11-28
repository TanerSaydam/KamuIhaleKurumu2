using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebApi.Dtos;
using MyFirstWebApi.Entities;
using MyFirstWebApi.Exceptions;

namespace MyFirstWebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]

public class TodosController : ControllerBase
{
    private static List<Todo> todos = new();

    [HttpPost]
    public IActionResult Create(CreateTodoDto request)
    {
        Todo todo = new()
        {
            Id = 1,
            Work = request.Work,
            IsCompleted = false,
            CreatedDate = DateTime.Now,
        };
        todos.Add(todo);

        return Ok(new { Message = "Kayıt işlemi başarıyla tamamlandı!" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
       
        return Ok(todos);
    }

    [HttpDelete("{index}")]
    public IActionResult DeleteByIndex(int index)
    {
        throw new NotFoundIndexException();
        todos.Remove(todos[index]);

        //database listeyi getir
        return Ok(new {Message = "Silme işlemi başarılı!"});
    }

    [HttpPut]
    public IActionResult Update(TodoUpdateDto request)
    {
        todos[request.Id].Work = request.Work;
        return Ok(new { Message = "Güncelleme işlemi başarılı" });
    }
}

public record TodoUpdateDto(
    int Id,
    string Work);

//public record Login(
//    string Email,
//    string Password);
//public class Login
//{
//    public Login(string email, string password)
//    {
//        Email = email;
//        Password = password;
//    }

//    public string Email { get; init; }
//    public string Password { get; init; }
//}
