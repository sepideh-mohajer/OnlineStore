using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using OnlineStore.Api.Extensions;
using OnlineStore.DataAccess;
using OnlineStore.Infrastructure.Filters;
using OnlineStore.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBusinessServices();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddScoped<ApiResultFilterAttribute>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//// Register AutoMapper by explicitly specifying the profiles
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
}

app.Run();
