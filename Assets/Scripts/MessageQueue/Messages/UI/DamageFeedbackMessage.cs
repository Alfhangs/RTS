using UnityEngine;

namespace RTS.Message.UI
{
    public class DamageFeedbackMessage : IMessage
    {
        public float Damage;
        public Vector3 Position;
    }
}
