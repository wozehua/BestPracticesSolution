using Serilog;
using System.Globalization;

Log.Logger = new LoggerConfiguration()
.WriteTo.File(
"logs/log.txt",
retainedFileCountLimit: 7,
rollingInterval: RollingInterval.Day, formatProvider: CultureInfo.InvariantCulture)
.MinimumLevel.Information()
.CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, config) =>
    {
        config.ReadFrom.Configuration(context.Configuration);
    });
    var app = builder.Build();

    app.MapGet("/", () => "Hello World!");

    await app.RunAsync();
}
catch (Exception ex) when (ex is not OutOfMemoryException and not StackOverflowException)
{
    Log.Error(ex, "在应用程序启动期间抛出以下 {Exception}", ex);
}
finally
{
    await Log.CloseAndFlushAsync();
}
