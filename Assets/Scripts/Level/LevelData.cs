using System.Collections.Generic;
using UnityEngine;
using Configuration;
using RTS.Enemy;

namespace RTS.Level
{
    [CreateAssetMenu(menuName = "Configuration/New Level")]
    public class LevelData : ScriptableObject
    {
        public List<LevelSlot> Slots = new List<LevelSlot>();
        public int Columns;
        public int Rows;
        public LevelConfiguration Configuration;
        public List<EnemyGroupConfiguration> EnemyGroups = new List<EnemyGroupConfiguration>();
    }
}