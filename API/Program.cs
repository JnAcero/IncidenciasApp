using System.Reflection;
using System.Text.Json.Serialization;
using API.Extensions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigCores();
builder.Services.ConfigureRateLimiting();
builder.Services.ConfigureApiVersioning();
builder.Services.AddControllers()
    .AddJsonOptions( options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAplicationServices();
builder.Services.AddDbContext<AplicationDbContext>(options =>{
        string ? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
    }
);
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
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
app.UseCors("CorsPolicy");
app.UseIpRateLimiting();
app.UseApiVersioning();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
