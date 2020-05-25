using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Take.Chat.Infrastructure.Handlers;
using Take.Chat.Interfaces.Business;
using Take.Chat.Interfaces.Repository;

namespace Take.Chat.Business
{
    public class ChannelBusiness : IChannelBusiness
    {
        private readonly IChannelRepository channelRepository;
        private WebSocketMessageHandler webSocketMessageHandler;

        public ChannelBusiness(IChannelRepository channelRepository, WebSocketMessageHandler webSocketMessageHandler)
        {
            this.channelRepository = channelRepository;
            this.webSocketMessageHandler = webSocketMessageHandler;
        }

        public void AddChannel(string channel) 
        {
            var channels = GetAllChannels();
            channels.Add(channel);
        }

        public IList<string> GetAllChannels()
        {
            return channelRepository.GetAllChannels();
        }

        public async Task UpdateChannelList()
        {
            //await webSocketMessageHandler.SendMessageToAll(JsonSerializer.Serialize(GetAllChannels()), null);
            //if (string.IsNullOrEmpty(channel))
            //{
            //    channel = "#general";
            //}

            //if (message.Message.StartsWith("/"))
            //{
            //    string[] strMessage = message.Message.Split(' ');

            //    switch (strMessage[0])
            //    {
            //        case "/to":
            //            message.Command = "/to";
            //            await webSocketMessageHandler.SendMessageToAll(JsonSerializer.Serialize(message), channel);

            //            break;
            //        case "/private":
            //            message.Command = "/private";
            //            await webSocketMessageHandler.SendMessage(strMessage[1], JsonSerializer.Serialize(message), channel);
            //            await webSocketMessageHandler.SendMessage(message.UserName, JsonSerializer.Serialize(message), channel);

            //            break;
            //        default:
            //            break;
            //    }
            //}
            //else
            //{
            //    await webSocketMessageHandler.SendMessageToAll(JsonSerializer.Serialize(message), channel);
            //}
        }
    }
}
