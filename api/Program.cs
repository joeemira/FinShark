using Microsoft.EntityFrameworkCore;
using api.Data;
using System.Runtime.CompilerServices;
using api.Interfaces;
using api.reposotry; // Make sure this matches your namespace

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStockReposotry, StockReposotry>();
builder.Services.AddScoped<IcommentsREposotry, CommentsRposotory>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
