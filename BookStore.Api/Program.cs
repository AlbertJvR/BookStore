using System.Globalization;
using System.Text.Json.Serialization;
using BookStore.Api.Common;
using BookStore.Application;
using BookStore.Application.Orders.Commands.CreateOrder;
using BookStore.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
    builder.Logging.AddEventSourceLogger();
}

// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddCors(options => { options.AddPolicy("AllowDefaultOrigin", config => config.AllowAnyOrigin()); });

builder.Services.AddControllers(options =>
    {
        options.EnableEndpointRouting = false;
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    })
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateOrderCommandHandler>())
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggeredApiVersioning();

var app = builder.Build();

// Setting localization is very important in containers
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(new CultureInfo("za"))
});

// Configure the HTTP request pipeline.
//TODO: I dont know if I wanna disable swagger in prod yet....
app.UseSwaggerWithVersioning();

app.UseCustomExceptionHandler();

app.UseCors("AllowDefaultOrigin");
app.UseRouting();

//app.UseHttpsRedirection();
//app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/healthz");

app.Run();