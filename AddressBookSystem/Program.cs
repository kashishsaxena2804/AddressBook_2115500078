using BusinessLayer.Interfaces;
using BusinessLayer.Mappings;
using BusinessLayer.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Database Connection
builder.Services.AddDbContext<AddressBookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Dependency Injection
builder.Services.AddScoped<IAddressBookRL, AddressBookRL>();
builder.Services.AddScoped<IAddressBookBL, AddressBookBL>();

// Add Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Address Book API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(AddressBookMappingProfile));
builder.Services.AddScoped<IValidator<AddressBookEntry>, AddressBookEntryValidator>();

var app = builder.Build();

// Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
