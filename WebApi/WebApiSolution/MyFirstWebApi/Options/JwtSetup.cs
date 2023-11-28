using Microsoft.Extensions.Options;

namespace MyFirstWebApi.Options;

public sealed class JwtSetup : IConfigureOptions<Jwt>
{
    private readonly IConfiguration _configuration;

    public JwtSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(Jwt options)
    {
       _configuration.GetSection("Jwt").Bind(options);
    }
}
