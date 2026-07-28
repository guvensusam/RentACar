using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Service;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();          // <-- 1. eksik olan
builder.Services.AddOpenApi();
builder.Services.AddScoped<IMarka, MarkaService>();
builder.Services.AddDbContext<RentACarDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "RentACar API v1");
    });
    
    
    
}

app.UseHttpsRedirection();

app.MapControllers();                        // <-- 2. eksik olan

app.Run();