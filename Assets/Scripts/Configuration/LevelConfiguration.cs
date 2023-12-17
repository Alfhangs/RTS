using System.Collections.Generic;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(menuName = "RTS/New Configuration")]
    public class LevelConfiguration : ScriptableObject
    {
        public List<LevelItem> levelItems = new List<LevelItem>();

        public LevelItem FindByType(LevelItemType type)
        {
            return levelItems.Find(item => item.Type == type);
        }
    }
}