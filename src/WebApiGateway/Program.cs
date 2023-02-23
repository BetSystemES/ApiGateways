using WebApiGateway.AppDependencies;
using WebApiGateway.Configuration;
using WebApiGateway.Configuration.SeriLog;
using WebApiGateway.Filters;
using WebApiGateway.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddSerialLogger();
builder.ConfigureDependencies();

builder.Services.AddControllers(options =>
{
    //options.Filters.Add<InputNullValidationFilter>();
    //options.Filters.Add<InputModelValidationFilter>();
    options.Filters.Add<InputNullValidationFilter2>();
    options.Filters.Add<InputModelValidationFilter2>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();

namespace WebApiGateway
{
    public partial class Program { }
}
