using CommonUtils.Constants;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// ConfigureResource 定义服务的元信息，例如服务名称：NewsletterApi
// 指标（Metrics）
// .AddAspNetCoreInstrumentation() 自动收集 ASP.NET Core 相关指标,如请求数、异常数、请求响应时间，响应状态码等
//.AddHttpClientInstrumentation() 自动收集 HttpClient 相关指标,如 HttpClient 请求数、HttpClient 异常数、HttpClient 响应时间等
//.AddOtlpExporter(options => options.Endpoint = new Uri(WebConstants.OpenTelemetryUrl)) 向 OpenTelemetry Collector 发送数据
// 通过OpenTelemetry协议（OTLP） 收集指标数据，并将数据发送到后端的 OpenTelemetry Collector
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(WebConstants.OpenTelemetryServiceName))
    .WithMetrics(metric =>
    {
        metric.AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri(WebConstants.OpenTelemetryUrl);
        });
    });
// Tracing 跟踪 类似于指标配置，主要用于记录请求流向及耗时
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing.AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri(WebConstants.OpenTelemetryUrl);
        });
    });
// 日志 将结构化日志导出到OTLP兼容的后端
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.AddOtlpExporter(options =>
    {
        options.Endpoint = new Uri(WebConstants.OpenTelemetryUrl);
    });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
