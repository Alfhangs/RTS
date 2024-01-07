using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Enemy
{
    [CreateAssetMenu(menuName = "RTS/New Enemy Group")]
    public class EnemyGroupData : ScriptableObject
    {
        public string Name;
        public List<EnemyData> Enemies = new List<EnemyData>();
    }
}