using GlosGuru.Api.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<GlosGuruContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GlosGuruDb")
        ?? throw new InvalidOperationException("Connection string 'postgresdb' not found.")));

var app = builder.Build();
app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//Create database and apply migrations
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<GlosGuruContext>();
//     dbContext.Database.Migrate();
// }

app.Run();
