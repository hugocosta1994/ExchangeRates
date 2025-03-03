using ExchangeRates.AlphaVantage;
using ExchangeRates.Data.Data;
using ExchangeRates.Data.Repository;
using ExchangeRates.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register ApplicationDbContext with the dependency injection container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddTransient<IUnitOfWorkDb, UnitOfWorkDb>();
builder.Services.AddTransient<IExchangeRateService, ExchangeRateService>();

// Register AlphaVantageService with the dependency injection container
builder.Services.AddTransient<AlphaVantageService>(provider =>
{
    var apiKey = builder.Configuration["AlphaVantage:ApiKey"];
    return new AlphaVantageService(apiKey);
});


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
