using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SGCAE.Api.Middleware;
using SGCAE.Application;
using SGCAE.Persistence;
using SGCAE.Persistence.Seeds;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

WebApplication app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    
    using (IServiceScope scope = app.Services.CreateScope())
    {
        await scope.ServiceProvider.GetRequiredService<SGCAEDbContext>().Database.MigrateAsync();
    }

    await DataBaseSeeder.SeedAsync(app.Services);
}

app.MapControllers();

app.Run();