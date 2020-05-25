using System;
using System.Threading.Tasks;
using Take.Chat.Domain.Dto;

namespace Take.Chat.Interfaces.Business
{
    public interface IChatMessagesBusiness
    {
        Task SendMessage(SendMessageDto message, string channel);
        void AddUserToChat(ChatUsersDto chatUser);
        bool IsUsernameInUse(string userName);
    }
}
