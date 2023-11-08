using InventoryMS.Client;
using InvoiceMS.Infrastructure.DataLayer;
using InvoiceMS.Infrastructure.MessageBroker;
using InvoiceMS.Infrastructure.Services;
using UsersMS.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IUserMsClient, UsersMsClient>();
builder.Services.AddScoped<IInvoiceDataLayer, InvoiceDataLayer>();

builder.Services.AddHostedService<InventoryMsEventsConsumer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
