using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<WeatherDbContext>(options => options.UseInMemoryDatabase("WeatherDb"));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddVersionedApiExplorer(option =>
{
    option.GroupNameFormat = "'v'V";
    option.SubstituteApiVersionInUrl = true;
});

builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = false;
    setupAction.DefaultApiVersion = new ApiVersion(1, 0);
    setupAction.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BrezyWeather OpenAPI",
        Version = "v1",
        Description = "OpenAPI Specification for BrezyWeather",
        License = new OpenApiLicense()
        {
            Name = "MIT",
        },
        Contact = new OpenApiContact()
        {
            Name = "Praveenkumar Bouna",
            Email = "hello@codewithpraveen.com"
        },
    });

    s.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "BrezyWeather OpenAPI",
        Version = "v2",
        Description = "OpenAPI Specification for BrezyWeather",
        License = new OpenApiLicense()
        {
            Name = "MIT",
        },
        Contact = new OpenApiContact()
        {
            Name = "Praveenkumar Bouna",
            Email = "hello@codewithpraveen.com"
        },
    });
});

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        foreach (var item in provider.ApiVersionDescriptions)
        {
            setupAction.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName);
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();