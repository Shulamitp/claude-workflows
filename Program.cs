var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

// Admin hello endpoint - requires authentication and Admin role
app.MapGet("/admin/hello", () => "Automated greeting from the admin endpoint.")
   .WithName("AdminHello")
   .RequireAuthorization("AdminOnly");

//
// Hello endpoint
app.MapGet("/hello", () => "Hello from .NET API!")
   .WithName("GetHello");

// Root endpoint
app.MapGet("/", () => "Welcome to Hello API - try /hello endpoint")
   .WithName("Root");

app.Run();
