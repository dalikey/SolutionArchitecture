using InvoiceManagement.Infrastructure;
using InvoiceManagement.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.





var mySQLConnectionString = builder.Configuration.GetConnectionString("mySQLConnection");
var developmentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Console.WriteLine("Environment: " + developmentEnvironment);


if (developmentEnvironment != "Development")
{
    mySQLConnectionString = mySQLConnectionString!.Replace("localhost", "mysqlserver");
}
Console.WriteLine("MySQL Connection String: " + mySQLConnectionString);

builder.Services.AddDbContext<InvoiceMySQLContext>(options =>
    options.UseMySql(mySQLConnectionString, new MySqlServerVersion(new Version(8, 0, 2))));


var rabbitMQHostName = builder.Configuration["RABBITMQ_HOSTNAME"];
#pragma warning disable ASP0012 // Suggest using builder.Services over Host.ConfigureServices or WebHost.ConfigureServices
builder.Host.ConfigureServices(services =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<OrderConfirmedConsumer>();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(rabbitMQHostName, "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            cfg.ReceiveEndpoint("invoice-management-queue", e =>
            {
                e.ConfigureConsumer<OrderConfirmedConsumer>(context);
                e.Bind("ballcom-exchange", x =>
                {
                    x.RoutingKey = "order-confirmed-routingkey";
                    x.ExchangeType = "topic";
                });
            });
        });
    });
});

builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<InvoiceRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
