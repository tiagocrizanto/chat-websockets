using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using Take.Chat.Infrastructure.Constants;
using Take.Chat.Interfaces.Repository;

namespace Take.Chat.Repository
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly IMemoryCache memoryCache;

        public ChannelRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IList<string> GetAllChannels() => memoryCache.Get<IList<string>>(CacheKeys.CHAT_CHANNELS);

        public void AddChannel(string channel)
        {
            //Recuperar todos os dados para não sobrescrever os dados do memory cache
            var allChannels = GetAllChannels();
            allChannels.Add(channel);
            memoryCache.Set(CacheKeys.CHAT_CHANNELS, allChannels);
        }
    }
}
