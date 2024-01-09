using System.Collections.Generic;
using UnityEngine;

namespace RTS.Message.UI
{
    public class UpdateDetailsMessage : IMessage
    {
        public List<UnitComponent> Units;
        public GameObject Model;
    }
}