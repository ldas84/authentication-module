using Auth.Application;
using Auth.Application.Interfaces;
using Auth.Infrastructure.Security;
using Auth.Domain.Interfaces;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Token Service
builder.Services.AddSingleton<ITokenService>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new TokenService(
        config["Jwt:Key"]!,
        config["Jwt:Issuer"]!,
        config["Jwt:Audience"]!,
        int.Parse(config["Jwt:ExpirationMinutes"] ?? "60")
    );
});

// HttpClient to AuthZ
builder.Services.AddHttpClient("AuthZ", client =>
{
    client.BaseAddress = new Uri("http://localhost:5145");
});

// Password hasher
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Auth.Application.AssemblyReference).Assembly));

// DbContext
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection"));
});

// Classic Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
