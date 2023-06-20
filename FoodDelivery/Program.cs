using FoodDelivery;
using FoodDelivery.DAL;
using FoodDelivery.DAL.Implementations;
using FoodDelivery.DAL.Interfaces;
using FoodDelivery.DAL.Repositories;
using FoodDelivery.Models.Entity;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
 



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllers();

builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            // ���������, ����� �� �������������� �������� ��� ��������� ������
//            ValidateIssuer = true,
//            // ������, �������������� ��������
//            ValidIssuer = AuthOptions.ISSUER,
//            // ����� �� �������������� ����������� ������
//            ValidateAudience = true,
//            // ��������� ����������� ������
//            ValidAudience = AuthOptions.AUDIENCE,
//            // ����� �� �������������� ����� �������������
//            ValidateLifetime = true,
//            // ��������� ����� ������������
//            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//            // ��������� ����� ������������
//            ValidateIssuerSigningKey = true,
//        };
//    });

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             }
                         },
                         new string[] { }
                     }
                });
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});


app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();


//public class AuthOptions
//{
//    public const string ISSUER = "MyAuthServer"; // �������� ������
//    public const string AUDIENCE = "MyAuthClient"; // ����������� ������
//    const string KEY = "mysupersecret_secretkey!123";   // ���� ��� ��������
//    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
//        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
//}