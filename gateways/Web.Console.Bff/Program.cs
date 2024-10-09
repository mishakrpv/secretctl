using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var envName = builder.Environment.EnvironmentName;

builder.Configuration.AddJsonFile(Path.Combine("configuration", $"ocelot.{envName}.json"));

services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration["Authentication:Authority"];
        options.TokenValidationParameters.ValidateAudience = false;
    });

services.AddOcelot();

var app = builder.Build();

app.UseAuthentication()
    .UseOcelot()
    .Wait();

app.Run();