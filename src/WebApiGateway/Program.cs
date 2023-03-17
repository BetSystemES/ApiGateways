using WebApiGateway.AppDependencies;
using WebApiGateway.Configuration;
using WebApiGateway.Configuration.Jwt;
using WebApiGateway.Filters;
using WebApiGateway.Middleware;
using WebApiGateway.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddSerialLogger();
builder.ConfigureDependencies();
var jwtConfig = builder.GetAppSettings<JwtConfig>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServicesConfiguration();
builder.Services.AddFilterConfiguration();
builder.Services.AddJwtAuthentication(jwtConfig);
builder.Services.AddAuthorizationPolicies();

builder.Services.AddAutoMapConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.SwaggerConfig();

app.UseHttpsRedirection();

app.UseAuthentication();   // добавление middleware аутентификации
app.UseAuthorization();

app.ConfigureExceptionMiddleware();

app.MapControllers();

app.Run();

namespace WebApiGateway
{
    public partial class Program
    { }
}