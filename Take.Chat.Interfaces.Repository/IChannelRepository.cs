using System.Collections.Generic;

namespace Take.Chat.Interfaces.Repository
{
    public interface IChannelRepository
    {
        IList<string> GetAllChannels();
        void AddChannel(string channel);
    }
}
