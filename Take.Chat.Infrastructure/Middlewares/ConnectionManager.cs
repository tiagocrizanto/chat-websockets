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
        private static ConcurrentDictionary<string, WebSocket> connections;

        public ConnectionManager()
        {
            if (connections == null)
                connections = new ConcurrentDictionary<string, WebSocket>();
        }

        public WebSocket GetSocketById(string id)
        {
            return connections.FirstOrDefault(x => x.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAllConnections()
        {
            return connections;
        }

        public string GetId(WebSocket socket)
        {
            return connections.FirstOrDefault(x => x.Value == socket).Key;
        }

        public async Task RemoveSocketAsync(string id)
        {
            connections.TryRemove(id, out WebSocket socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
        }

        public void AddSocket(WebSocket socket)
        {
            var socketId = Guid.NewGuid().ToString();
            connections.TryAdd(socketId, socket);
        }

        public void AddSocket(WebSocket socket, string socketId)
        {
            connections.TryAdd(socketId, socket);
        }
    }
}
