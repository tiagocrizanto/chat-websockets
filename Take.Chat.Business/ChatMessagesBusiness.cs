using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Take.Chat.Business.Mappers;
using Take.Chat.Domain.Dto;
using Take.Chat.Domain.Entity;
using Take.Chat.Infrastructure.Handlers;
using Take.Chat.Infrastructure.Middlewares;
using Take.Chat.Interfaces.Business;
using Take.Chat.Interfaces.Repository;

namespace Take.Chat.Business
{
    public class ChatMessagesBusiness : IChatMessagesBusiness
    {
        private readonly IChatUsersRepository chatUsersRepository;
        private readonly IChannelRepository channelRepository;
        private WebSocketMessageHandler webSocketMessageHandler;
        private ConnectionManager connectionManager;

        public ChatMessagesBusiness(IChatUsersRepository chatUsersRepository, WebSocketMessageHandler webSocketMessageHandler, ConnectionManager connectionManager,
            IChannelRepository channelRepository)
        {
            this.chatUsersRepository = chatUsersRepository;
            this.webSocketMessageHandler = webSocketMessageHandler;
            this.connectionManager = connectionManager;
            this.channelRepository = channelRepository;
        }

        public void AddUserToChat(ChatUsersDto chatUser)
        {
            List<ChatUsers> chatUsers = chatUsersRepository.GetAllChatUsers();
            if (chatUsers == null)
                chatUsers = new List<ChatUsers>();

            chatUsers.Add(ChatMessageMapper.ToChatUserEntity(chatUser));
            chatUsersRepository.AddUserToChat(chatUsers);
        }

        public bool IsUsernameInUse(string userName)
        {
            IEnumerable<ChatUsers> chatUsers = chatUsersRepository.GetAllChatUsers();

            if (chatUsers != null && chatUsers.Any(x => x.Name == userName))
                return true;

            return false;
        }

        public async Task SendMessage(SendMessageDto message)
        {
            if(message.Message.StartsWith("/"))
            {
                string[] strMessage = message.Message.Split(' ');

                switch (strMessage[0])
                {
                    case "/to":
                        message.Command = "/to";
                        await webSocketMessageHandler.SendMessageToAll(JsonSerializer.Serialize(message), message.Channel);
                        
                        break;
                    case "/private":
                        message.Command = "/private";
                        await webSocketMessageHandler.SendMessage(strMessage[1], JsonSerializer.Serialize(message), message.Channel);
                        await webSocketMessageHandler.SendMessage(message.UserName, JsonSerializer.Serialize(message), message.Channel);

                        break;
                    default:
                        break;
                }
            }
            else
            {
                await webSocketMessageHandler.SendMessageToAll(JsonSerializer.Serialize(message), message.Channel);
            }
        }

        public void CreateChannel(string channel)
        {
            channelRepository.AddChannel(channel);
            var c = channelRepository.GetAllChannels().FirstOrDefault();
            var connections = connectionManager.GetAllConnections(c);

            foreach (var conn in connections)
            {
                connectionManager.AddSocket(conn.Value, conn.Key, channel);
            }
        }
    }
}
