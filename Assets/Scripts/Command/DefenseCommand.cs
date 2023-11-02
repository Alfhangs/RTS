using Configuration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseCommand : ICommand
{
    public void Execute()
    {
        MessageQueueManager.Instance.SendMessage(new ActionCommandMessage { Action = ActionType.Defense });
    }
}
