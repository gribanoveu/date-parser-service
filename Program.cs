using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов контроллеров
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();

