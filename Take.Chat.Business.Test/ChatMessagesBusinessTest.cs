using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Take.Chat.Domain.Dto;
using Take.Chat.Domain.Entity;
using Take.Chat.Infrastructure.Handlers;
using Take.Chat.Infrastructure.Middlewares;
using Take.Chat.Interfaces.Repository;
using Xunit;

namespace Take.Chat.Business.Test
{
    public class ChatMessagesBusinessTest
    {
        private readonly Mock<IChatUsersRepository> chatUsersRepository;
        private ConnectionManager connectionManager;
        private WebSocketMessageHandler webSocketMessageHandler;
        public ChatMessagesBusinessTest()
        {
            connectionManager = new ConnectionManager();
            webSocketMessageHandler = new WebSocketMessageHandler(connectionManager);
            chatUsersRepository = new Mock<IChatUsersRepository>();
        }

        [Fact(DisplayName = "Used username")]
        public void IsUsernameInUse_UsernameInUse()
        {
            //Arrange
            chatUsersRepository.Setup(x => x.GetAllChatUsers()).Returns(new List<ChatUsers>
            {
                new ChatUsers
                {
                    Id = 1,
                    Name = "Tiago"
                },
                new ChatUsers
                {
                    Id = 2,
                    Name = "Crizanto"
                },
                new ChatUsers
                {
                    Id = 3,
                    Name = "Take"
                }
            });
            var chatMessageBusiness = new ChatMessagesBusiness(chatUsersRepository.Object, webSocketMessageHandler, connectionManager);

            //Act
            bool result = chatMessageBusiness.IsUsernameInUse("Tiago");

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "No used username")]
        public void IsUsernameInUse_UsernameNotInUse()
        {
            //Arrange
            chatUsersRepository.Setup(x => x.GetAllChatUsers()).Returns(new List<ChatUsers>
            {
                new ChatUsers
                {
                    Id = 1,
                    Name = "Tiago"
                },
                new ChatUsers
                {
                    Id = 2,
                    Name = "Crizanto"
                },
                new ChatUsers
                {
                    Id = 3,
                    Name = "Take"
                }
            });

            var chatMessageBusiness = new ChatMessagesBusiness(chatUsersRepository.Object, webSocketMessageHandler, connectionManager);

            //Act
            bool result = chatMessageBusiness.IsUsernameInUse("TakeChat");

            //Assert
            Assert.False(result);
        }
    }
}
