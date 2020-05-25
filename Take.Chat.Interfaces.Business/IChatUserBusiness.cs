using System.Collections.Generic;
using Take.Chat.Domain.Dto;

namespace Take.Chat.Interfaces.Business
{
    public interface IChatUserBusiness
    {
        ChatUsersDto GetUserByUserName(string userName);
        IEnumerable<ChatUsersDto> GetAllChatUsers();
        void RemoveUserFromSession(string userName);
    }
}
