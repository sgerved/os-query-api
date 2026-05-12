using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using os_query_api.BusinessLogic;
using os_query_api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<os_query_api.BusinessLogic.IOperatingSystemInformationLogic, os_query_api.BusinessLogic.OperatingSystemInformationLogic>();
builder.Services.AddScoped<os_query_api.BusinessLogic.IShell, os_query_api.BusinessLogic.Shell>();
builder.Services.AddDbContext<os_query_api.DataAccess.ApiEventsDbContext>();
builder.Services.AddScoped<os_query_api.DataAccess.IApiEventRepository, os_query_api.DataAccess.ApiEventRepository>();
builder.Services.AddSingleton<ITokenCacheSingleton, TokenCacheSingleton>();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

KeycloakOptions keycloakOptions = new();
builder.Configuration.GetSection("Keycloak").Bind(keycloakOptions);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = keycloakOptions.Url;
        options.Audience = keycloakOptions.Audience;
        options.RequireHttpsMetadata = keycloakOptions.RequireHttpsMetadata;
        options.MapInboundClaims = false;
        options.TokenValidationParameters.RoleClaimType = keycloakOptions.RoleClaimType;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();