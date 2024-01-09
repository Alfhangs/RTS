namespace RTS.Message.UI
{
    public class UpdateResourceMessage : IMessage
    {
        public int Amount;
        public ResourceType Type;
    }
}