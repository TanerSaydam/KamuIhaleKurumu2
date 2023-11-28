using DataAccess;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        ApplicationDbContext context = new();
        //context.Set<CreditCard>().Add(new()
        //{
        //    CartType = "KK",
        //    Number = "50",
        //    Owner = "Taner"
        //});
        //context.SaveChanges();
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var response1 = context.Set<BankAccount>().ToList();
        stopwatch.Stop();

        var result = stopwatch.ElapsedMilliseconds;
        //context.Set<BillingDetail>().Add(
        //    new()
        //    {
        //        Number = "100",
        //        Owner = "Test"
        //    });

        //context.SaveChanges();

        //var response = context
        //    .Set<BillingDetail>()
        //    .Where(p => EF.Property<string>(p, "Type") == "BillingDetail")
        //    .ToList();
        //BenchmarkRunner.Run<BenchmarkService>();
        //var response = context.Categories.Include(p => p.Products).AsSplitQuery().ToList();

        Console.ReadLine();
    }
}