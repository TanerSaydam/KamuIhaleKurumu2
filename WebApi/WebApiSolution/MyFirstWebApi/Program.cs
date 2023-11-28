using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyFirstWebApi.Conctext;
using MyFirstWebApi.Entities;
using MyFirstWebApi.IoC;
using MyFirstWebApi.Middleware;
using MyFirstWebApi.Options;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//string connectionString = builder.Configuration.GetSection("SqlServer").Value;

//builder.Services.AddDbContext<AppDbContext>(cfr =>
//    cfr.UseSqlServer(connectionString));

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtSetup>();

//builder.Services.AddAuthentication().AddJwtBearer(opt =>
//{
//    opt.TokenValidationParameters = new()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateIssuerSigningKey = true,
//        ValidateLifetime = true,
//        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
//        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:SecretKey").Value)),
//        //ClockSkew = TimeSpan.FromMinutes(10)
//    };
//});
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(cfr =>
    {
        cfr
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(cfr => true)
        .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.CreateService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddTransient<ExceptionMiddleware>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use((context, next) =>
{
    
    return next(context);
});

app.CreateMiddleware();

app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    AppDbContext context = new();
    if (!context.Set<User>().Any())
    {
        User user = new()
        {
            UserName = "test",
            Provider = Guid.NewGuid().ToString()
        };

        context.Set<User>().Add(user);
        context.SaveChanges();
    }
}

//app.Use(async (context, next) =>
//{
//    try
//    {
//        await next(context);
//    }
//    catch (Exception ex)
//    {
//        context.Response.ContentType = "application/json";
//        context.Response.StatusCode = 500;

//        var errorResponse = new
//        {
//            StatusCode = context.Response.StatusCode,
//            Message = ex.Message
//        };

//        var error = JsonConvert.SerializeObject(errorResponse);

//        await context.Response.WriteAsync(error);
//    }
//});

app.Run();
