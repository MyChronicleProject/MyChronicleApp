using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Application.FamilyTrees;
using MyChronicle.Infrastructure;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("DockerConnection");
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:7072");
    });
});

builder.Services.AddMediatR(cfg =>
   cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Create>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;

var retryPolicy = Policy.Handle<Exception>().WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (exception, timeSpan, retryCount, context) =>
{
    var logger = service.GetRequiredService<ILogger<Program>>();
    logger.LogError(exception, $"Retry {retryCount} encountered an error: {exception.Message}. Waiting {timeSpan} before retrying...");
});

try
{
    var context = service.GetRequiredService<DataContext>();

    await retryPolicy.Execute(async () =>
    {
        context.Database.Migrate();
        await Seed.SeedData(context);
    });
}
catch (Exception ex)
{
    var logger = service.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}


app.Run();
