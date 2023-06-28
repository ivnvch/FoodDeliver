using FoodDelivery;
using FoodDelivery.Configuration;
using FoodDelivery.DAL;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Host.ConfigureLogging(logigng =>
{
    logigng.ClearProviders();
    logigng.SetMinimumLevel(LogLevel.Trace);
}).UseNLog();


builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.RegistrationSwagger();

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseAuthorization();

app.MapControllers();

app.Run();
