using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configuration;

namespace Level
{
    [CreateAssetMenu(menuName = "Configuration/New Level")]
    public class LevelData : ScriptableObject
    {

        public List<LevelSlot> Slots = new List<LevelSlot>();
        public int Columns;
        public int Rows;
        public LevelConfiguration Configuration;
    }
}