using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Take.Chat.Infrastructure.Middlewares;

namespace Take.Chat.Infrastructure.Handlers
{
    public class WebSocketMessageHandler : SocketHandler
    {
        public WebSocketMessageHandler(ConnectionManager connections)
            : base(connections)
        {

        }

        public override async Task OnConnected(WebSocket socket, string socketId)
        {
            await base.OnConnected(socket, socketId);
            //string socketId = Connections.GetId(socket);
            //await SendMessageToAll($"{socketId} joined the room");
        }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            string socketId = Connections.GetId(socket);
            //var message = $"{socketId} said {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            //await SendMessageToAll(message);
        }
    }
}
