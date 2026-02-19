using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Authentication and authorization
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSection["Key"]!))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

//
// Hello endpoint
app.MapGet("/hello", () => "Hello from .NET API!")
   .WithName("GetHello");

// Root endpoint
app.MapGet("/", () => "Welcome to Hello API - try /hello endpoint")
   .WithName("Root");

// Admin hello endpoint - requires authentication and Admin role
app.MapGet("/admin/hello", () => "Automated admin hello from .NET API!")
   .WithName("GetAdminHello")
   .RequireAuthorization(policy => policy.RequireRole("Admin"));

app.Run();
