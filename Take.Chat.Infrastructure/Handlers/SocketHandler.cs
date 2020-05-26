using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Take.Chat.Infrastructure.Middlewares;

namespace Take.Chat.Infrastructure.Handlers
{
    public abstract class SocketHandler
    {
        public ConnectionManager Connections { get; set; }

        public SocketHandler(ConnectionManager connections)
        {
            Connections = connections;
        }

        public virtual async Task OnConnected(WebSocket socket, string userName)
        {
            await Task.Run(() =>
            {
                Connections.AddSocket(socket, userName, "#general");
            });
        }

        public virtual async Task OnConnected(WebSocket socket, string socketId, string channel)
        {
            await Task.Run(() =>
            {
                Connections.AddSocket(socket, socketId, channel);
            });
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await Connections.RemoveSocketAsync(Connections.GetId(socket, null));
        }

        public async Task SendMessage(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message), 0, message.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task SendMessage(string id, string message, string channel)
        {
            var socket = Connections.GetSocketById(channel, id);
            await SendMessage(socket, message);
        }

        public async Task SendMessageToAll(string message, string channel)
        {
            foreach (var con in Connections.GetAllConnections(channel))
            {
                await SendMessage(con.Value, message);
            }
        }

        public abstract Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
