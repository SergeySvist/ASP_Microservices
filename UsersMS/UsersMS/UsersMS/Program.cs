using System.Text.Json;
using UsersMS.Cache;
using UsersMS.Domain.DataLayer;
using UsersMS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddTransient<IUserDataLayer, UserDataLayer>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserCacheClient, UserCacheClient>();

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
