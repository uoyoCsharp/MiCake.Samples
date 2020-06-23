namespace MiCakeDemoApplication.Dto.Chat
{
    public class ChatMsgItem
    {
        public int Type { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        public bool IsRead { get; set; }

        public bool SendSuccess { get; set; }
    }
}
