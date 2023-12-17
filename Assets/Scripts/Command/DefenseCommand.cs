using Configuration;

public class DefenseCommand : ICommand
{
    public void Execute()
    {
        MessageQueueManager.Instance.SendMessage(new ActionCommandMessage { Action = ActionType.Defense });
    }
}
