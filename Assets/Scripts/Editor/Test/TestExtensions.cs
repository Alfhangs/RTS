using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Test
{
    public static class TestExtensions
    {
        public static float GetAttack(this UnitData data, int level)
        {
            return Mathf.Pow(level, data.levelMultiplier) + data.attack;
        }
        public static float GetDefense(this UnitData data, int level)
        {
            return Mathf.Pow(level, data.levelMultiplier) + data.defense;
        }
    }
}