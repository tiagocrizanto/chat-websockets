using System.Collections.Generic;
using Take.Chat.Domain.Entity;

namespace Take.Chat.Interfaces.Repository
{
    public interface IChatUsersRepository
    {
        void AddUserToChat(IEnumerable<ChatUsers> chatUser);
        List<ChatUsers> GetAllChatUsers();
        ChatUsers GetUserByLogin(string userName);
        void DeleteUser(string userName);
    }
}
