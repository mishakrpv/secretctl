using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Web.Services.Bff.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var envName = builder.Environment.EnvironmentName;

builder.Configuration.AddJsonFile(Path.Combine("configuration", $"ocelot.{envName}.json"));

builder.Services.AddHttpClient<AccessManagementService>(
    c => c.BaseAddress = new Uri("http://access-management"));

services.AddOcelot();

var app = builder.Build();
    
app.UseMiddleware<Web.Services.Bff.Middleware.AccessManagementMiddleware>();
app.UseOcelot().Wait();

app.Run();