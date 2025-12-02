using ByteGuard.Codex.Core.Configuration;
using ByteGuard.Codex.Infrastructure.Sqlite.Configuration;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCodexCore();
builder.Services.AddCodexSqliteStorage(builder.Configuration.GetConnectionString("DatabaseContext")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseStaticFiles();
app.MapFallbackToFile("index.html");

//app.UseAuthorization();
//app.UseAuthentication();

app.MapControllers();

app.Run();
