using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UseMediatR.MediatRModel;
using UseMediatR.Notification;

var builder = WebApplication.CreateBuilder(args);

//ע�� MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/register", async (UserRegistrationRequest request, IMediator mediator) =>
{
    var userId = new Random().Next(1, 10000);
    await mediator.Publish(new UserRegisteredNotification(userId, request.Email));
    return Results.Ok(new { Message = "�û�ע��ɹ�!", UserId = userId });
});

await app.RunAsync();

