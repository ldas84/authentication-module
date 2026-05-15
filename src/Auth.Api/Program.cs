using Auth.Application;
using Auth.Application.Interfaces;
using Auth.Infrastructure.Security;
using Auth.Domain.Interfaces;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(">>> CADENA DE CONEXIÓN LEÍDA POR AUTH.API:");
Console.WriteLine(builder.Configuration.GetConnectionString("AuthConnection"));

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
builder.Services.AddMediatR(typeof(Auth.Application.AssemblyReference).Assembly);

// DbContext
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("AuthConnection"));
});

// Authentication + JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});

// Authorization
builder.Services.AddAuthorization();

// Swagger
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

app.UseRouting();

// Authentication + Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

