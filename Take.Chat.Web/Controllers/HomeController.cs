using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using Take.Chat.Domain.Dto;
using Take.Chat.Interfaces.Business;

namespace Take.Chat.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatMessagesBusiness chatMessagesBusiness;
        private readonly IChatUserBusiness chatUserBusiness;
        private readonly IChannelBusiness channelBusiness;
        public HomeController(IChatMessagesBusiness chatMessagesBusiness, IChatUserBusiness chatUserBusiness, IChannelBusiness channelBusiness)
        {
            this.chatMessagesBusiness = chatMessagesBusiness;
            this.chatUserBusiness = chatUserBusiness;
            this.channelBusiness = channelBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string userName)
        {
            if (chatMessagesBusiness.IsUsernameInUse(userName))
            {
                TempData["NAME_IN_USE"] = true;
                return View();
            }

            chatMessagesBusiness.AddUserToChat(new ChatUsersDto
            {
                Id = Guid.NewGuid(),
                Name = userName
            });

            return RedirectToAction(nameof(Chat), new { username = userName });
        }

        [HttpGet]
        public IActionResult Chat([FromQuery] string userName)
        {
            return View(nameof(Chat), userName);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto messageObj)
        {
            await chatMessagesBusiness.SendMessage(messageObj);
            return Ok();
        }

        [HttpPost]
        public IActionResult Logoff(string userName)
        {
            chatUserBusiness.RemoveUserFromSession(userName);
            return Ok();
        }

        [HttpGet]
        public JsonResult ConnectedUsers()
        {
            return Json(chatUserBusiness.GetAllChatUsers());
        }

        [HttpGet]
        public JsonResult ChatChannels()
        {
            IList<string> channels = channelBusiness.GetAllChannels();

            return Json(channels);
        }
    }
}
