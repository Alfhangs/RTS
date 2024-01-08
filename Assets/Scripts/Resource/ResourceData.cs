using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Resources
{
    [CreateAssetMenu(menuName = "RTS/New Resource")]
    public class ResourceData : ScriptableObject
    {
        public int ProductionPerSecond;
        public int ProductionLevel;
        public ResourceType Type;
    }
}