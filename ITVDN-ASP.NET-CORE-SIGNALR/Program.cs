using ITVDN_ASP.NET_CORE_SIGNALR.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7049");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
    });
});
var app = builder.Build();

app.UseCors();

app.MapHub<ChatHub>("/chat");

app.Run();
