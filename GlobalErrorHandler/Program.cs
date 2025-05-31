using GlobalErrorHandler;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
var app = builder.Build();

app.UseMiddleware<ExceptionHandingMiddleware>();
app.UseExceptionHandler();
app.MapGet("/", () => "Hello World!");

await app.RunAsync();
