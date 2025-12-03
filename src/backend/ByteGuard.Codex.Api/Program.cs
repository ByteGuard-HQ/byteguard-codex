using Microsoft.AspNetCore.Mvc;
using ByteGuard.Codex.Core.Configuration;
using ByteGuard.Codex.Infrastructure.Sqlite.Configuration;
using Scalar.AspNetCore;
using ByteGuard.Codex.Api.Converters;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Add custom converters.
        options.JsonSerializerOptions.Converters.Add(new AsvsCodeJsonConverter());
    });

builder.Services.AddOpenApi();
builder.Services.AddHttpLogging(o => { });

builder.Services.AddCodexCore();
builder.Services.AddCodexSqliteStorage(builder.Configuration.GetConnectionString("DatabaseContext")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("ByteGuard Codex - API documentation");
        options.WithFavicon("/favicon.ico");
    });
}

app.MapControllers();

//app.UseAuthorization();
//app.UseAuthentication();

app.UseHttpLogging();

app.UseStaticFiles();

app.Run();
