using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApi.Conctext;
using MyFirstWebApi.Dtos;
using MyFirstWebApi.Entities;
using Newtonsoft.Json.Linq;

namespace MyFirstWebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
//[Authorize(AuthenticationSchemes ="Bearer")]
//[Role]
public class TodoPostController : ControllerBase
{
    private AppDbContext context = new();
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TodoPostController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoDto request, CancellationToken cancellationToken)
    {
        Todo todo = new()
        {
            Work = request.Work,
            IsCompleted = false,
            CreatedDate = DateTime.Now,
        };
        
        await context.Set<Todo>().AddAsync(todo, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Ok(new { Message = "Kayıt işlemi başarıyla tamamlandı!" });
    }

    [HttpGet]
    public async Task<IActionResult> SyncElasticSearch()
    {
        var setting = new ConnectionConfiguration(new Uri("http://localhost:9200"));
        var client = new ElasticLowLevelClient(setting);

        List<Todo> todos = await context.Set<Todo>().ToListAsync();

        var tasks = new List<Task>();
        foreach (var todo in todos)
        {
            var response = 
                await client
                .GetAsync<StringResponse>("todos", todo.Id.ToString());

            if(response.HttpStatusCode != 200)
            {
                tasks.Add(client.IndexAsync<StringResponse>("todos", todo.Id.ToString(), PostData.Serializable(todo)));
            }            
        }

        await Task.WhenAll(tasks);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByElasticSearh()
    {
        var setting = new ConnectionConfiguration(new Uri("http://localhost:9200"));
        var client = new ElasticLowLevelClient(setting);

        var response = await client.SearchAsync<StringResponse>("todos", PostData.Serializable(new
        {
            size = 10000,
            query = new
            {
                match_all = new { }
            }
        }));

        //var response = await client.SearchAsync<StringResponse>("todos", PostData.Serializable(new
        //{
        //    query = new
        //    {
        //        wildcard = new
        //        {
        //            Work = new { value = $"" }
        //        }
        //    }
        //}));
        //select * from Todos where Work like '%%'  //SQL Query karşılığı

        var result = JObject.Parse(response.Body);
        var hits = result["hits"]["hits"].ToObject<List<JObject>>();

        List<Todo> todos = new();

        foreach (var hit in hits)
        {
            todos.Add(hit["_source"].ToObject<Todo>());
        }

        return Ok(todos.Count());
    }

    [HttpPost]
    public async Task<IActionResult> GetAll()
    {
        //List<Todo> list = new List<Todo>();
        //for (int i = 0; i < 10000; i++)
        //{
        //    Todo todo = new()
        //    {
        //        Work = "Todo Work " + i + Guid.NewGuid(),
        //        IsCompleted = false,
        //        CreatedDate = DateTime.Now,
        //    };

        //    list.Add(todo);
        //}

        //context.Set<Todo>().AddRange(list);
        //context.SaveChanges();


        var result = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        List<Todo> todos = await context.Set<Todo>().AsNoTracking().ToListAsync();
        return Ok(todos);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> DeleteById(int id, CancellationToken cancellationToken)
    {
        Todo todo = context.Set<Todo>().Find(id);
        context.Set<Todo>().Remove(todo);
        await context.SaveChangesAsync(cancellationToken);

        //database listeyi getir
        return Ok(new { Message = "Silme işlemi başarılı!" });
    }

    [HttpPost]
    public async Task<IActionResult> Update(TodoUpdateDto request, CancellationToken cancellationToken)
    {
        Todo todo = context.Set<Todo>().Where(x => x.Id == request.Id).FirstOrDefault();
        todo.Work = request.Work;
        
        await context.SaveChangesAsync(cancellationToken);

        return Ok(new { Message = "Güncelleme işlemi başarılı" });
    }
}

