using System.Reflection;
using AccessManagement.Authentication;
using AccessManagement.Data;
using AccessManagement.Data.Repositories;
using Cassandra;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo
    .Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));

services.AddControllers();

// const string scheme = "SCTL-SHA256";
// services.AddAuthentication(scheme)
//     .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>(
//         scheme, options => { });

services.AddSingleton<ICluster>(Cluster.Builder()
    .AddContactPoint(config["Cassandra:ContactPoint"])
    .WithPort(int.Parse(config["Cassandra:Port"] ?? throw new InvalidOperationException(
        "Could not parse Cassandra port")))
    .WithCredentials(config["Cassandra:User"], config["Cassandra:Password"])
    .Build());

services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = config.GetConnectionString("MySql");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 39));
    options.UseMySql(connectionString, serverVersion)
        .LogTo(Log.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

services.AddScoped<CassandraSessionCache>();
services.AddScoped<IPolicyRepository, CassandraPolicyRepository>();

services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

app.MapControllers();

app.Run();