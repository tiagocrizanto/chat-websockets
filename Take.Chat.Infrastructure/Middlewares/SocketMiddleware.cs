using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Take.Chat.Infrastructure.Handlers;

namespace Take.Chat.Infrastructure.Middlewares
{
    public class SocketMiddleware
    {
        private readonly RequestDelegate _next;
        private SocketHandler _webSocketHandler { get; set; }

        public SocketMiddleware(RequestDelegate next, SocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;


            var socket = await context.WebSockets.AcceptWebSocketAsync();
            string userName = context.Request.Query["userName"];

            if (string.IsNullOrEmpty(userName))
                await _webSocketHandler.OnConnected(socket);
            else
                await _webSocketHandler.OnConnected(socket, userName);


            await Receive(socket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    await _webSocketHandler.Receive(socket, result, buffer);
                    return;
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocketHandler.OnDisconnected(socket);
                    return;
                }

            });
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                        cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}
