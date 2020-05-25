using System.Net.WebSockets;
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

        public override async Task OnConnected(WebSocket socket, string socketId, string channel)
        {
            await base.OnConnected(socket, socketId, channel);
        }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
        }
    }
}
