using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Message.UI
{
    public class UpdateResourceMessage : IMessage
    {
        public int Amount;
        public ResourceType Type;
    }
}