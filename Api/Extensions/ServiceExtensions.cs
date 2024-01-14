using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace Api.Extensions;

public static class ServiceExtensions
{
  public static void ConfigureCors(this IServiceCollection services) =>
    services.AddCors(options =>
    {
      options.AddPolicy("CorsPolicy", builder =>
      builder.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());
    });

  public static void ConfigureSwagger(this IServiceCollection services)
  {
    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1",
        new OpenApiInfo
        {
          Title = "Customer Case Service API",
          Description = "API microservice for CRM Customer Case Service",
          Version = "v1"
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

    services.AddSwaggerExamplesFromAssemblyOf(typeof(ServiceExtensions));
  }
}
