using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using Take.Chat.Domain.Entity;
using Take.Chat.Infrastructure.Constants;
using Take.Chat.Interfaces.Repository;

namespace Take.Chat.Repository
{
    public class ChatUserRepository : IChatUsersRepository
    {
        private readonly IMemoryCache memoryCache;
        public ChatUserRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void AddUserToChat(IEnumerable<ChatUsers> chatUser) => memoryCache.Set(CacheKeys.CONNECTED_USERS, chatUser);
        
        public List<ChatUsers> GetAllChatUsers() => memoryCache.Get<List<ChatUsers>>(CacheKeys.CONNECTED_USERS);
        public ChatUsers GetUserByLogin(string userName)
        {
           IEnumerable<ChatUsers> users = memoryCache.Get<List<ChatUsers>>(CacheKeys.CONNECTED_USERS);
            return users.FirstOrDefault(x => x.Name == userName);
        }

        public void DeleteUser(string userName)
        {
            var users = GetAllChatUsers().RemoveAll(x => x.Name == userName);
            memoryCache.Set(CacheKeys.CONNECTED_USERS, users);
        }
    }
}
