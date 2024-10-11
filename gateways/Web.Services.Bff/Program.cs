using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Web.Services.Bff.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;
var envName = builder.Environment.EnvironmentName;

config.AddJsonFile(Path.Combine("configuration", $"ocelot.{envName}.json"));

builder.Services.AddHttpClient<AccessManagementService>(
    c => c.BaseAddress = new Uri(config["IAM:URL"]!));

services.AddOcelot();

var app = builder.Build();
    
app.UseMiddleware<Web.Services.Bff.Middleware.AccessManagementMiddleware>();
app.UseOcelot().Wait();

app.Run();