var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Only add controllers for now, or even remove this initially
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Start with just a simple route to ensure the app can even run
// This will bypass most middleware
app.MapGet("/", () => "Hello from Vercel ASP.NET Core!");

app.Run();