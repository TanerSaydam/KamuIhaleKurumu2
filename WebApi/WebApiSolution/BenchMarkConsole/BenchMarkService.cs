using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Elasticsearch.Net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApi.Entities;
using Newtonsoft.Json.Linq;

namespace BenchMarkConsole;

[ShortRunJob, Config(typeof(Config))]
public class BenchMarkService
{
    private class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithRatioStyle(BenchmarkDotNet.Columns.RatioStyle.Trend);
        }
    }

    //[Benchmark(Baseline = true)]
    //public void ToList()
    //{
    //    AppDbContext context = new();
    //    List<Todo> todos = context.Todos.ToList();
    //}

    [Benchmark(Baseline =true)]
    public void NoTrackingToList()
    {
        AppDbContext context = new();
        List<Todo> todos = context.Todos.AsNoTracking().Take(10000).ToList();
    }

    [Benchmark]
    public void ElasticSearchAllList()
    {
        var setting = new ConnectionConfiguration(new Uri("http://localhost:9200"));
        var client = new ElasticLowLevelClient(setting);

        var response = client.Search<StringResponse>("todos", PostData.Serializable(new
        {
            size = 10000,
            query = new
            {
                match_all = new { }
            }
        }));

        //select * from Todos where Work like '%%'  //SQL Query karşılığı

        var result = JObject.Parse(response.Body);
        var hits = result["hits"]["hits"].ToObject<List<JObject>>();

        List<Todo> todos = new();

        foreach (var hit in hits)
        {
            todos.Add(hit["_source"].ToObject<Todo>());
        }

    }

    //[Benchmark]
    //public async Task ToListAsync()
    //{
    //    AppDbContext context = new();
    //    List<Todo> todos = await context.Todos.ToListAsync();
    //}

    //[Benchmark]
    //public async Task NoTrackingToListAsync()
    //{
    //    AppDbContext context = new();
    //    List<Todo> todos = await context.Todos.AsNoTracking().ToListAsync();
    //}

    //[Benchmark]
    //public async Task SqlQuery()
    //{
    //    SqlConnection connection = new("Data Source=TUGAY\\SQLEXPRESS;Initial Catalog=MyTodoDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    //    string query = "Select * From Todos";
    //    SqlCommand cmd = new(query,connection); 
    //    connection.Open();
    //    cmd.ExecuteReader();
    //    connection.Close();

    //}

    //[Benchmark]
    //public async Task EFSQLQuery()
    //{
    //    AppDbContext context = new();
    //    context.Todos.FromSqlRaw("Select * From Todos");

    //}
}
