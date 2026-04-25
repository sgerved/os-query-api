var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<os_query_api.BusinessLogic.IOperatingSystemInformationLogic, os_query_api.BusinessLogic.OperatingSystemInformationLogic>();
builder.Services.AddScoped<os_query_api.BusinessLogic.IShell, os_query_api.BusinessLogic.Shell>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();