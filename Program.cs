using Microsoft.EntityFrameworkCore;
using SellerReturnApi.Data;
using SellerReturnApi.Models;

var builder = WebApplication.CreateBuilder(args);

Add services
builder.Services.AddDbContextApplicationDbContext(options =
    options.UseNpgsql(builder.Configuration.GetConnectionString(DefaultConnection)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Применить миграции при старте (только для dev!)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredServiceApplicationDbContext();
    context.Database.Migrate(); автоматически применит миграции
}

app.Run();