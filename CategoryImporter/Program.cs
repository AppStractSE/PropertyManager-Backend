using CategoryImporter;
using Core;
using Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(Program).Assembly);

builder.InitCore(config);
builder.InitInfrastructure(false, true);

builder.Services.AddHostedService<Importer>();

var app = builder.Build();

app.Run();
