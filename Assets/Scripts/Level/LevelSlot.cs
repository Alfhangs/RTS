using UnityEngine;
using System;
using Configuration;

namespace RTS.Level
{
    [Serializable]
    public class LevelSlot
    {

        public LevelItemType ItemType;
        public Vector2Int Coordinates;
        public LevelSlot(LevelItemType type, Vector2Int coordinates)
        {
            ItemType = type;
            Coordinates = coordinates;
        }
    }
}