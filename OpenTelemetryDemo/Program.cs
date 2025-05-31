using CommonUtils.Constants;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// ConfigureResource ��������Ԫ��Ϣ������������ƣ�NewsletterApi
// ָ�꣨Metrics��
// .AddAspNetCoreInstrumentation() �Զ��ռ� ASP.NET Core ���ָ��,�����������쳣����������Ӧʱ�䣬��Ӧ״̬���
//.AddHttpClientInstrumentation() �Զ��ռ� HttpClient ���ָ��,�� HttpClient ��������HttpClient �쳣����HttpClient ��Ӧʱ���
//.AddOtlpExporter(options => options.Endpoint = new Uri(WebConstants.OpenTelemetryUrl)) �� OpenTelemetry Collector ��������
// ͨ��OpenTelemetryЭ�飨OTLP�� �ռ�ָ�����ݣ��������ݷ��͵���˵� OpenTelemetry Collector
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
// Tracing ���� ������ָ�����ã���Ҫ���ڼ�¼�������򼰺�ʱ
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
// ��־ ���ṹ����־������OTLP���ݵĺ��
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
