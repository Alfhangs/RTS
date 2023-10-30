using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateResourceMessage : IMessage
{
    public int Amount;
    public ResourceType Type;
}
