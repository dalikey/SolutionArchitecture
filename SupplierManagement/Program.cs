using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using RabbitMQ.domain;
using SupplierManagement.Domain.Events;
using SupplierManagement.Infrastructure;
using SupplierManagement.Services;

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

builder.Services.AddDbContext<SupplierMySQLContext>(options => options.UseMySql(mySQLConnectionString, new MySqlServerVersion(new Version(8, 0, 2))));

var mongoDBConnectionString = builder.Configuration.GetConnectionString("MongoDBConnection");
var client = new MongoClient(mongoDBConnectionString);
var database = client.GetDatabase("ReadSupplier");
builder.Services.AddSingleton(database);
builder.Services.AddScoped<SupplierMongoDBContext>();


var rabbitMQHostName = builder.Configuration["RABBITMQ_HOSTNAME"];
#pragma warning disable ASP0012 // Suggest using builder.Services over Host.ConfigureServices or WebHost.ConfigureServices
builder.Host.ConfigureServices(services =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<ProductConsumer>();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(rabbitMQHostName, "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            cfg.ReceiveEndpoint("supplier-management-queue", e =>
            {
                e.ConfigureConsumer<ProductConsumer>(context);
                e.Bind("ballcom", x =>
                {
                    x.RoutingKey = "product-inserted-routingkey";
                    x.ExchangeType = "topic";
                });
            });

            cfg.Message<IInsertedEvent>(x => { x.SetEntityName("product-inserted-event"); });
            cfg.Publish<IInsertedEvent>(x => { x.ExchangeType = "topic"; });

        });
    });
    // Add the bus to the container

    //services.AddHostedService<Worker>();
});

builder.Services.AddScoped<ProductConsumer>();

// Add other services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<SupplierRepository>();
builder.Services.AddScoped<SupplierService>();

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

Console.WriteLine("Supplier management is running!");
Console.WriteLine("Running in environment: " + app.Environment.EnvironmentName);


//var eventConsumer = new EventConsumer();
//eventConsumer.ConsumeEvents<SupplierRegisteredEvent>();

app.Run();
