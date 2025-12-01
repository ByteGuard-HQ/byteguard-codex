using ByteGuard.Codex.Core.Configuration;
using ByteGuard.Codex.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.AddCodexCore();
builder.Services.AddSqlite(builder.Configuration.GetConnectionString("DatabaseContext")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.Run();
