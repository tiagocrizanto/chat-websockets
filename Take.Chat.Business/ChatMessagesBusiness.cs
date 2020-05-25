using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Take.Chat.Business.Mappers;
using Take.Chat.Domain.Dto;
using Take.Chat.Domain.Entity;
using Take.Chat.Interfaces.Business;
using Take.Chat.Interfaces.Repository;
using Take.Chat.Infrastructure.Handlers;
using Take.Chat.Infrastructure.Middlewares;

namespace Take.Chat.Business
{
    public class ChatMessagesBusiness : IChatMessagesBusiness
    {
        private readonly IChatUsersRepository chatUsersRepository;
        private WebSocketMessageHandler webSocketMessageHandler;
        private ConnectionManager connectionManager;

        public ChatMessagesBusiness(IChatUsersRepository chatUsersRepository, WebSocketMessageHandler webSocketMessageHandler,
            ConnectionManager connectionManager)
        {
            this.chatUsersRepository = chatUsersRepository;
            this.webSocketMessageHandler = webSocketMessageHandler;
            this.connectionManager = connectionManager;
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
                        await webSocketMessageHandler.SendMessageToAll(JsonSerializer.Serialize(message));
                        
                        break;
                    case "/private":
                        message.Command = "/private";
                        await webSocketMessageHandler.SendMessage(strMessage[1], JsonSerializer.Serialize(message));
                        await webSocketMessageHandler.SendMessage(message.UserName, JsonSerializer.Serialize(message));

                        break;
                    default:
                        break;
                }
            }
            else
            {
                await webSocketMessageHandler.SendMessageToAll(JsonSerializer.Serialize(message));
            }
        }
    }
}
