using System.Collections.Generic;
using Take.Chat.Domain.Dto;
using Take.Chat.Domain.Entity;
using System.Linq;

namespace Take.Chat.Business.Mappers
{
    /// <summary>
    /// Mapper criado para evitar a instalação de uma dependência
    /// Aqui poderia utiliza o AutoMapper ao invés de fazer o mapeamento manual
    /// </summary>
    public static class ChatMessageMapper
    {
        public static ChatUsers ToChatUserEntity(ChatUsersDto chatUsersDto)
        {
            return new ChatUsers
            {
                Id = 1,
                Name = chatUsersDto.Name
            };
        }

        public static ChatUsersDto ToChatUserDto(ChatUsers chatUsersDto)
        {
            return new ChatUsersDto
            {
                Id = 2,
                Name = chatUsersDto.Name
            };
        }

        public static IEnumerable<ChatUsersDto> ToChatUserDtoList(IEnumerable<ChatUsers> chatUsers)
        {
            return chatUsers.Select(x => new ChatUsersDto
            {
                Id = 3,
                Name = x.Name
            });
        }
    }
}
