using ITVDN_ASP.NET_CORE_SIGNALR.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();

var app = builder.Build();
app.MapControllers();
app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapHub<ChatHub>("/chat");
app.MapHub<EvenMinuteHub>("/even");
app.Run();
