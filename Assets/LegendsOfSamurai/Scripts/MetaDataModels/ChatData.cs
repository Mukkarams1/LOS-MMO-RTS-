
    public class CityChatting
    {
        public string _id { get; set; }
        public string fromName { get; set; }
        public bool isMyself { get; set; }
        public string avatar { get; set; }
        public string message { get; set; }
        public string createdAt { get; set; }
    }

    public class ClanChatting
    {
        public string _id { get; set; }
        public string fromName { get; set; }
        public bool isMyself { get; set; }
        public string avatar { get; set; }
        public string message { get; set; }
        public string createdAt { get; set; }
    }

    public class ChatData
    {
        public ClanChatting[] ClanChatting { get; set; }
        public CityChatting[] CityChatting { get; set; }
    }


