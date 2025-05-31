using UseDotNetChannels.Channels;
using UseDotNetChannels.EventBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<InMemoryMessageQueue>();
builder.Services.AddSingleton<IEventBus, EventBus>();
builder.Services.AddHostedService<IntergrationEventProcessJob>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


await app.RunAsync();
