namespace ITVDN_ASP.NET_CORE_SIGNALR.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string username, string message);
    Task UserConnected(string connectionId);
    Task UserDisconnected(string connectionId);
}