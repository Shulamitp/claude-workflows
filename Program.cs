using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Fake auth for demo purposes
builder.Services.AddAuthorization();

var app = builder.Build();

// Dev OpenAPI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
//
//
app.MapGet("/secure", [Authorize] () =>
{
    return "This endpoint requires authorization";
})
.WithName("SecureEndpoint");


app.MapGet("/hello", () =>
{
    return "Hello from insecure endpoint!";
})
.WithName("InsecureHello");


app.MapGet("/", () =>
{
    return "Root endpoint without authorization";
})
.WithName("Root");


app.Run();
