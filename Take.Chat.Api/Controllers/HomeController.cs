using Microsoft.AspNetCore.Mvc;
using Take.Chat.Interfaces.Business;

namespace Take.Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IChatMessagesBusiness chatMessagesBusiness;
        public HomeController(IChatMessagesBusiness chatMessagesBusiness)
        {
            this.chatMessagesBusiness = chatMessagesBusiness;
        }


    }
}