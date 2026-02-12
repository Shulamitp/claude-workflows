var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//
// Hello endpoint
app.MapGet("/hello", () => "Hello from .NET API!")
   .WithName("GetHello");

// Root endpoint
app.MapGet("/", () => "Welcome to Hello API - try /hello endpoint")
   .WithName("Root");

app.Run();
