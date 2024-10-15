using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using TrackingManagement.Infrastructure;
using TrackingManagement.Services;
using RabbitMQ.domain;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

var mySQLConnectionString = builder.Configuration.GetConnectionString("MySQLConnection");
var developmentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (developmentEnvironment != "Development")
{
    mySQLConnectionString = mySQLConnectionString.Replace("localhost", "mysqlserver");
}
Console.WriteLine("MySQL Connection String: " + mySQLConnectionString);

builder.Services.AddDbContext<TrackingMySQLContext>(options => options.UseMySql(mySQLConnectionString, new MySqlServerVersion(new Version(8, 0, 2))));

var mongoDBConnectionString = builder.Configuration.GetConnectionString("MongoDBConnection");
var client = new MongoClient(mongoDBConnectionString);
var database = client.GetDatabase("ReadTracking");
builder.Services.AddSingleton(database);
builder.Services.AddScoped<TrackingMongoDBContext>();

var rabbitMQHostName = builder.Configuration["RABBITMQ_HOSTNAME"];
#pragma warning disable ASP0012 // Suggest using builder.Services over Host.ConfigureServices or WebHost.ConfigureServices
builder.Host.ConfigureServices(services =>
{
    services.AddMassTransit(x =>
    {
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(rabbitMQHostName, "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            
            cfg.Message<ITrackingStartedEvent>(x =>
            {
                x.SetEntityName("ballcom-exchange");
            });
            cfg.Publish<ITrackingStartedEvent>(x =>
            {
                x.ExchangeType = "topic";
            });
            cfg.Message<ITrackingUpdatedEvent>(x =>
            {
                x.SetEntityName("ballcom-exchange");
            });
            cfg.Publish<ITrackingUpdatedEvent>(x =>
            {
                x.ExchangeType = "topic";
            });
        });
    });
});
#pragma warning restore ASP0012 // Suggest using builder.Services over Host.ConfigureServices or WebHost.ConfigureServices

// Add other services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<TrackingService>();
builder.Services.AddScoped<TrackingRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

Console.WriteLine("Tracking management is running!");
Console.WriteLine("Running in environment: " + app.Environment.EnvironmentName);

app.Run();