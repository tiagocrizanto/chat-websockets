namespace Take.Chat.Domain.Dto
{
    public class SendMessageDto
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public string Command { get; set; }
        public string Channel { get; set; }
    }
}
