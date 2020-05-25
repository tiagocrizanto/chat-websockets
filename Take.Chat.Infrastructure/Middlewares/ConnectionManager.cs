using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Take.Chat.Infrastructure.Middlewares
{
    public class ConnectionManager
    {
        private static ConcurrentDictionary<string, ConcurrentDictionary<string, WebSocket>> connections;

        public ConnectionManager()
        {
            if (connections == null)
                connections = new ConcurrentDictionary<string, ConcurrentDictionary<string, WebSocket>>();
        }

        public WebSocket GetSocketById(string channel, string id)
        {
            var channelSocket = connections.FirstOrDefault(x => x.Key == channel);

            return connections[channel].FirstOrDefault(x => x.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAllConnections(string channel)
        {
            return connections[channel];
        }

        public string GetId(WebSocket socket, string channel)
        {
            return connections[channel].FirstOrDefault(x => x.Value == socket).Key;
        }

        public async Task RemoveSocketAsync(string id, string channel)
        {
            connections[channel].TryRemove(id, out WebSocket socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
        }

        public void AddSocket(WebSocket socket, string channel)
        {
            var socketId = Guid.NewGuid().ToString();
            connections[channel].TryAdd(socketId, socket);
        }

        public void AddSocket(WebSocket socket, string socketId, string channel)
        {
            if (connections.ContainsKey(channel))
            {
                connections[channel].TryAdd(socketId, socket);
            }
            else
            {
                AddChannel(socket, socketId, channel);
            }
        }

        public void AddChannel(WebSocket socket, string socketId, string channelName)
        {
            var socketChannel = new ConcurrentDictionary<string, WebSocket>();
            socketChannel.TryAdd(socketId, socket);
            connections.TryAdd(channelName, socketChannel);
        }
    }
}
