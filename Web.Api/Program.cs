using Microsoft.VisualBasic;
using Web.Api.Middlerware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddTransient<FactoryMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
// 请求委托中间件
app.Use(async (context, next) =>
{
    ArgumentNullException.ThrowIfNull(context);
    ArgumentNullException.ThrowIfNull(next);
    await next(context);
});

app.UseMiddleware<ConventionMiddleware>();
app.UseMiddleware<FactoryMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
