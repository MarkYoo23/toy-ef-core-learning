using Microsoft.EntityFrameworkCore;
using Toy.EfCore.Learning.Applications.Services.Databases;
using Toy.EfCore.Learning.Controllers.Extensions.Extensions;
using Toy.EfCore.Learning.Controllers.Filters;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Infrastructures.Contexts;
using Toy.EfCore.Learning.Infrastructures.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Database Contexts
var schoolConnectionString = builder.Configuration.GetConnectionString("School");
builder.Services.AddLoggerFactoryDbContext<SchoolContext>(schoolConnectionString);

var blogConnectionString = builder.Configuration.GetConnectionString("Blog");
builder.Services.AddLoggerFactoryDbContext<BlogContext>(blogConnectionString);

// Repositories
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();

// Services
builder.Services.AddScoped<IDatabaseInitializeService, DatabaseMigrationService>();
builder.Services.AddBlogServices();

// Filters
builder.Services.AddScoped<GlobalExceptionFilter>();

builder.Services.AddControllers(options => { options.Filters.Add<GlobalExceptionFilter>(); });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var databaseInitializeService = serviceProvider.GetRequiredService<IDatabaseInitializeService>();
    var dbContexts = new DbContext[]
    {
        serviceProvider.GetRequiredService<SchoolContext>(),
        serviceProvider.GetRequiredService<BlogContext>()
    };

    await databaseInitializeService.InitializeAsync(dbContexts);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public abstract partial class Program
{
}