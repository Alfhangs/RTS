using RTS.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RTS.Test
{
    public class BaseTest
    {
        protected UnitData LoadUnit(string unit)
        {
            return AssetDatabase.LoadAssetAtPath<UnitData>($"Assets/Scriptables/Unit/{unit}.asset");
        }

        protected EnemyData LoadEnemy(string enemy)
        {
            return AssetDatabase.LoadAssetAtPath<EnemyData>($"Assets/Scriptables/Enemy/{enemy}.asset");
        }

        protected EnemyGroupData LoadEnemyGroup(string enemyGroup)
        {
            return AssetDatabase.LoadAssetAtPath<EnemyGroupData>($"Assets/Scriptables/EnemyGroup/{enemyGroup}.asset");
        }
    }
}