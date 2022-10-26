using Microsoft.EntityFrameworkCore.SqlServer;
using CoAP_Backend_Api.Database;
using Microsoft.EntityFrameworkCore;
using CoAP_Backend_Api.Repositories;
using CoAP_Backend_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WeatherContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("*");
    });
});
    
builder.Services.AddScoped<IWeatherStationRepository, WeatherStationRepository>();
builder.Services.AddScoped<IWeatherStationServices, WeatherStationServices>();

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

app.UseCors(MyAllowSpecificOrigins);

app.Run();
