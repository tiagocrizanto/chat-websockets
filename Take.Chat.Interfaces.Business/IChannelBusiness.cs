using System.Collections.Generic;

namespace Take.Chat.Interfaces.Business
{
    public interface IChannelBusiness
    {
        IList<string> GetAllChannels();
        void AddChannel(string channel);
    }
}
