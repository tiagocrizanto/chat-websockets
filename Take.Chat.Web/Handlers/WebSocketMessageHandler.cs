using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Take.Chat.Web.Middlewares;

namespace Take.Chat.Web.Handlers
{
    public class WebSocketMessageHandler : SocketHandler
    {
        public WebSocketMessageHandler(ConnectionManager connections)
            : base(connections)
        {

        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
            string socketId = Connections.GetId(socket);
            await SendMessageToAll($"{socket} joined the room");
        }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            string socketId = Connections.GetId(socket);
            var message = $"{socketId} said {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            await SendMessageToAll(message);
        }
    }
}
