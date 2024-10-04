using Microsoft.Extensions.Configuration;
using System.Linq;
using WebApplication2.Models;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);
// Реєстрація сервісів у DI-контейнері
builder.Services.AddTransient<CalcService>();  // Ваш сервіс для арифметичних операцій
builder.Services.AddTransient<TimeService>();  // Ваш сервіс для аналізу часу

// Додаємо підтримку контролерів (необхідно для API)
builder.Services.AddControllers();

var app = builder.Build();

// Додаємо middleware для обробки запитів
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Middleware для кореневої сторінки ("/"), що відразу повертає результати
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        // Отримуємо сервіси через DI
        var calcService = context.RequestServices.GetService<CalcService>();
        var timeService = context.RequestServices.GetService<TimeService>();

        // Використовуємо сервіси для обчислень
        int resultAdd = calcService.Add(5, 3); // Наприклад, додавання 5 + 3
        string timeOfDay = timeService.GetTimeOfDay(); // Отримуємо результат аналізу часу

        // Встановлюємо заголовок Content-Type з кодуванням UTF-8
        context.Response.ContentType = "text/html; charset=utf-8";

        // Відправляємо результат як HTML-сторінку
        await context.Response.WriteAsync("<html><head><meta charset='UTF-8'></head><body>");
        await context.Response.WriteAsync($"<h1>Арифметичні операції</h1>");
        await context.Response.WriteAsync($"<p>Результат додавання 5 + 3= {resultAdd}</p>");
        await context.Response.WriteAsync($"<h1>Аналіз часу</h1>");
        await context.Response.WriteAsync($"<p>{timeOfDay}</p>");
        await context.Response.WriteAsync("</body></html>");
    }
    else
    {
        await next();
    }
});

app.UseRouting();

// Налаштовуємо контролери, якщо ви маєте інші API
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();  // Підключаємо контролери
});

app.Run();