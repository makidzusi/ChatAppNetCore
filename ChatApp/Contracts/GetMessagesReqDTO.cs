namespace ChatApp.Contracts
{
    public class GetMessagesReqDTO
    {
        public int recipientId { get; set; }
        public int senderId { get; set; }
    }
}
