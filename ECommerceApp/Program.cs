using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------------------------------
// Add repositories
// -----------------------------------------------------------------------------
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

// -----------------------------------------------------------------------------
// OpenAPI / Swagger services  (⬅️ added)
// -----------------------------------------------------------------------------
builder.Services.AddOpenApi(); // minimal-API spec generator (was already here)
builder.Services.AddEndpointsApiExplorer(); // discovers endpoints for Swagger
builder.Services.AddSwaggerGen(opts => // JSON + UI
{
    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "E-Commerce API",
        Version = "v1"
    });
});

builder.Services.AddControllers();
// -----------------------------------------------------------------------------
// Database
// -----------------------------------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// -----------------------------------------------------------------------------
// HTTP request pipeline
// -----------------------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    // JSON spec endpoint (was already here)
    app.MapOpenApi();

    // Swagger UI  (⬅️ added)
    app.UseSwagger(); // serves /swagger/v1/swagger.json
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Commerce API v1");
        c.RoutePrefix = "docs"; // UI at /docs  →  https://host/docs
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();