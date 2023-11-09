using InventoryMS.Client;
using InvoiceMS.Infrastructure;
using InvoiceMS.Infrastructure.DataLayer;
using InvoiceMS.Infrastructure.EventProcessors;
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

builder.Services.AddTransient<IInvoiceDataLayer, InvoiceDataLayer>();
builder.Services.AddTransient<IInventoryMsEventsProcessor, InventoryMsEventsProcessor>();
builder.Services.AddTransient<IInventoryItemUpdatesNotificationsProcessor, InvoiceService>();

builder.Services.AddSingleton<IUserMsClient, UsersMsClient>();

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
