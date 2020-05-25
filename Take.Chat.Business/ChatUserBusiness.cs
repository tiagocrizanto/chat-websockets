using System.Collections.Generic;
using Take.Chat.Business.Mappers;
using Take.Chat.Domain.Dto;
using Take.Chat.Interfaces.Business;
using Take.Chat.Interfaces.Repository;

namespace Take.Chat.Business
{
    public class ChatUserBusiness : IChatUserBusiness
    {
        private readonly IChatUsersRepository chatUsersRepository;
        public ChatUserBusiness(IChatUsersRepository chatUsersRepository)
        {
            this.chatUsersRepository = chatUsersRepository;
        }

        public IEnumerable<ChatUsersDto> GetAllChatUsers()
        {
            return ChatMessageMapper.ToChatUserDtoList(chatUsersRepository.GetAllChatUsers());
        }

        public ChatUsersDto GetUserByUserName(string userName)
        {
            return ChatMessageMapper.ToChatUserDto(chatUsersRepository.GetUserByLogin(userName));
        }

        public void RemoveUserFromSession(string userName)
        {
            chatUsersRepository.DeleteUser(userName);
        }
    }
}
