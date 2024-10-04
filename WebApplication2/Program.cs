using Microsoft.Extensions.Configuration;
using System.Linq;
using WebApplication2.Models;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);
// ��������� ������ � DI-���������
builder.Services.AddTransient<CalcService>();  // ��� ����� ��� ������������ ��������
builder.Services.AddTransient<TimeService>();  // ��� ����� ��� ������ ����

// ������ �������� ���������� (��������� ��� API)
builder.Services.AddControllers();

var app = builder.Build();

// ������ middleware ��� ������� ������
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Middleware ��� �������� ������� ("/"), �� ������ ������� ����������
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        // �������� ������ ����� DI
        var calcService = context.RequestServices.GetService<CalcService>();
        var timeService = context.RequestServices.GetService<TimeService>();

        // ������������� ������ ��� ���������
        int resultAdd = calcService.Add(5, 3); // ���������, ��������� 5 + 3
        string timeOfDay = timeService.GetTimeOfDay(); // �������� ��������� ������ ����

        // ������������ ��������� Content-Type � ���������� UTF-8
        context.Response.ContentType = "text/html; charset=utf-8";

        // ³���������� ��������� �� HTML-�������
        await context.Response.WriteAsync("<html><head><meta charset='UTF-8'></head><body>");
        await context.Response.WriteAsync($"<h1>���������� ��������</h1>");
        await context.Response.WriteAsync($"<p>��������� ��������� 5 + 3= {resultAdd}</p>");
        await context.Response.WriteAsync($"<h1>����� ����</h1>");
        await context.Response.WriteAsync($"<p>{timeOfDay}</p>");
        await context.Response.WriteAsync("</body></html>");
    }
    else
    {
        await next();
    }
});

app.UseRouting();

// ����������� ����������, ���� �� ���� ���� API
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();  // ϳ�������� ����������
});

app.Run();