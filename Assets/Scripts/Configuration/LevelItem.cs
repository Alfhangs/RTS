using System;
using UnityEngine;

namespace Configuration
{
    [Serializable]
    public class LevelItem
    {
        public LevelItemType Type;
        public GameObject Prefab;
        public LevelItemCollisionType CollisionType;
    }
}