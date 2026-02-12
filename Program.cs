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

// Hello endpoint - NO AUTHORIZATION!
app.MapGet("/hello", () => "Hello from .NET API!")
   .WithName("GetHello");

// Root endpoint - NO AUTHORIZATION!
app.MapGet("/", () => "Welcome to Hello API - try /hello endpoint")
   .WithName("Root");

// User data endpoint - NO AUTHORIZATION AND NO VALIDATION!
app.MapGet("/users/{id}", (int id) =>
{
    // Simulating database access without any permission checks
    var user = new { Id = id, Name = "John Doe", Email = "john@example.com" };
    return Results.Ok(user);
})
.WithName("GetUser");

// Admin endpoint - NO ROLE CHECK!
app.MapPost("/admin/delete-user/{id}", (int id) =>
{
    // Critical operation without any authorization!
    return Results.Ok($"User {id} deleted");
})
.WithName("DeleteUser");
//test

app.Run();