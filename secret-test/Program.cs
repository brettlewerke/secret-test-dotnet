using secret_test.Services;
using secret_test.Authentication;
using secret_test.Observability;
using secret_test.ErrorHandling;
using secret_test.Configuration;
using secret_test.Swagger;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// create basic console logging to provide visibility into configuration setup
var logger = ObservabilityExtension.GetBasicConsoleLogger<Program>();
builder.UseAzureAppConfiguration(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithAuthentication(builder.Configuration.GetSection("Swagger"), logger);

builder.Services.AddHttpClient();
builder.AddDependencyChecks();
builder.AddAuthentication(builder.Configuration.GetSection("Authentication"));

// Add business services
builder.Services.AddSingleton<IInsightService, InsightService>();
builder.Services.Configure<InsightServiceConfig>(builder.Configuration.GetSection(InsightServiceConfig.Config));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithOptions(builder.Configuration);
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
//Enable dev-certs and local HTTPS debugging first: ``app.UseHttpsRedirection();``
app.UseAuthorization();
app.MapControllers();
app.MapHealthCheckEndpoint();

await app.RunAsync();
