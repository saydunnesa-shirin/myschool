using System.Reflection;
using System.Text.Json.Serialization;
using Api.Features.Employees;
using Api.Infrastructure;
using Api.Infrastructure.Authorization;
using Api.Infrastructure.Cache;
using Api.Infrastructure.Correlation;
using Api.Infrastructure.Exceptions;
using Api.Infrastructure.Logging;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
  options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<GetEmployee>();
builder.Services.AddHealthChecks();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1",
    new OpenApiInfo
    {
      Title = "Customer Case Service API",
      Description = "API microservice for CRM Customer Case Service", Version = "v1"
    });
  options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
  options.CustomSchemaIds(type =>
    type.ToString().Replace(type.Namespace + ".", "").Replace("+", "."));

  options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
  {
    Description = "ApiKey must appear in header",
    Type = SecuritySchemeType.ApiKey,
    Name = "X-Api-Key",
    In = ParameterLocation.Header,
    Scheme = "ApiKeyScheme"
  });
  var key = new OpenApiSecurityScheme
  {
    Reference = new OpenApiReference
    {
      Type = ReferenceType.SecurityScheme,
      Id = "ApiKey"
    },
    In = ParameterLocation.Header
  };
  var requirement = new OpenApiSecurityRequirement
  {
    { key, new List<string>() }
  };
  options.AddSecurityRequirement(requirement);

  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
  options.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.Converters.Add(
    new JsonStringEnumConverter(new UpperCaseEnumValueNamingPolicy()));
  options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddCors(options =>
{
  options.AddPolicy("CorsPolicy", builder =>
  {
    builder.AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod();
  });
});

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (new[] { "Development", "Staging", "Production" }.Contains(env))
  //builder.Services.AddStackExchangeRedisCache(options =>
  //{
  //    var vaultSecretsConfig = builder.Configuration.GetSection("VaultSecretsConfiguration");
  //    var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
  //    options.Configuration = redisConnectionString.Replace("__REDIS_SECRET__", vaultSecretsConfig.GetValue<string>("REDIS_SECRET"));
  //});
  builder.Services.AddDistributedMemoryCache();
else
  builder.Services.AddDistributedMemoryCache();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
builder.Services.Configure<AppConfiguration>(builder.Configuration.GetSection("AppConfiguration"));
builder.Services.Configure<VaultSecretsConfiguration>(
  builder.Configuration.GetSection("VaultSecretsConfiguration"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();

builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();

// Serilog with Two-stage initialization, latter inits from appsettings.json
Log.Logger = new LoggerConfiguration()
  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateBootstrapLogger();
builder.Host.UseSerilog((context, services, configuration) => configuration
  .ReadFrom.Configuration(context.Configuration)
  .ReadFrom.Services(services)
);

Log.Information("Building services");

var app = builder.Build();


// App Configuration
if (app.Environment.IsDevelopment())
{
  // pragma exclusion is for a false-positive SQ flag asking to confirm this is ran only outside of Production environment
#pragma warning disable S4507
  app.UseDeveloperExceptionPage();
#pragma warning restore S4507
}

if (app.Environment.IsEnvironment("LocalDevelopment"))
{
  builder.Configuration.AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
  app.UseDeveloperExceptionPage();
}

// Global error handling
app.AddGlobalErrorHandler();


// Serilog RequestLogging - important to invoke here, before MVC calls
app.UseSerilogRequestLogging(opts =>
  {
    opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequest;
    opts.GetLevel = LogHelper.ExcludeHealthChecks;
    opts.IncludeQueryInRequestPath = true;
    opts.MessageTemplate =
      "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed} ms";
  }
);

//API
app.MapControllers();
app.MapHealthChecks("/health");
app.UseSwagger(options =>
{
  options.PreSerializeFilters.Add((doc, req) =>
  {
    var basePath = "/api";
    doc.Servers = new List<OpenApiServer> { new() { Url = $"https://{req.Host.Value}{basePath}" } };
  });
});
app.UseSwaggerUI();
app.UsePathBase("/api");

// Security
app.UseAuthorization();
app.UseRouting();
app.UseCors("CorsPolicy");

app.UseMiddleware<CorrelationIdBehavior>();
app.UseMiddleware<ApiKeyMiddleware>();

//if (!app.Environment.IsEnvironment("LocalDevelopment"))
//{
//    // Elastic APM, use *Subscribers to monitor more functions the agent will listen to
//    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
//    app.UseElasticApm(config,
//        new HttpDiagnosticsSubscriber(),
//        new AspNetCoreDiagnosticSubscriber());
//}

// Startup with Log.CloseAndFlush
try
{
  Log.Information("Starting web host");
  app.Run();
  return 0;
}
catch (Exception ex)
{
  Log.Fatal(ex, "Host terminated unexpectedly");
  return 1;
}
finally
{
  Log.CloseAndFlush();
}

namespace Api
{
  // documented by Microsoft and necessary for test framework
  public class Program
  {
  }
}
