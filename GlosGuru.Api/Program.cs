using GlosGuru.Api.Model;
using GlosGuru.Api.Repositories;
using GlosGuru.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Update the connection string to use a dynamic port resolution
// var postgresPort = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
// var connectionString = $"Host=localhost;Port={postgresPort};Database=GlosGuru;Username=yourusername;Password=yourpassword";
// builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure DbContext with PostgreSQL
builder.Services.AddDbContext<GlosGuruContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GlosGuruDb")));

// Register repositories and services
builder.Services.AddScoped<IWordListRepository, WordListRepository>();
builder.Services.AddScoped<IWordListService, WordListService>();

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
