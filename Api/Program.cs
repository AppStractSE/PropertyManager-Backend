using Api;
using Domain;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.EFCore.Context;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Setup mapster
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(Program).Assembly);

builder.InitDomain(config);
builder.InitInfrastructure();

var mapperConfig = new Mapper(config);
builder.Services.AddSingleton<IMapper>(mapperConfig);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.ResponsePropertiesAndHeaders;
    logging.MediaTypeOptions.AddText("application/json");
});

builder.Services.InitAuth(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Create mock data for Db
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var pmContext = scopedProvider.GetRequiredService<PropertyManagerContext>();
        await SeedDb.SeedAsync(pmContext);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.Run();
