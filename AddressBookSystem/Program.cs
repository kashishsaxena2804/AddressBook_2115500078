using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using StackExchange.Redis;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add DbContext with SQL Server
builder.Services.AddDbContext<AddressBookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Register Dependencies
builder.Services.AddScoped<IAddressBookBL, AddressBookBL>();
builder.Services.AddScoped<IAddressBookRL, AddressBookRL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserRL, UserRL>();
builder.Services.AddScoped<IEmailService, EmailServiceBL>();

//Redis
var redisConnection = ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]);
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
builder.Services.AddScoped<ICacheService, CacheService>();


// 🔹 Configure JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = key
        };
    });

// Add Redis Session
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // Redis server address
    options.InstanceName = "Session_"; // Optional instance name
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();



// 🔹 Add Controllers
builder.Services.AddControllers();

// 🔹 Add Swagger with JWT Support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AddressBook API", Version = "v1" });

    // Enable XML Documentation
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    // Add JWT Auth to Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });
});


builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new ConnectionFactory
    {
        HostName = config["RabbitMQ:Host"],
        UserName = config["RabbitMQ:Username"],
        Password = config["RabbitMQ:Password"]
    };
});


// 🔹 Build & Run
var app = builder.Build();

Task.Run(() =>
{
    var consumer = new RabbitMQConsumer();
    consumer.StartConsuming();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
