using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;
using UnityEngine;
using UnityEngine.UIElements;

namespace RTS.Test
{
    public class BattleSingleTest : BaseTest
    {
        private static List<(string, string, int, bool)> battles = new List<(string, string, int, bool)>
        {
            ("DefaultMage", "DefaultOrc", 0, false),
            ("DefaultWarrior", "DefaultGolem", 0, false),
            ("DefaultMage", "DefaultGolem", 0, false),
            ("DefaultWarrior", "DefaultDragon", 0, false),
            ("DefaultMage", "DefaultDragon", 0, false),
            ("DefaultWarrior", "DefaultOrc", 10, true),
            ("DefaultMage", "DefaultOrc", 1, true),
            ("DefaultWarrior", "DefaultGolem", 1, false),
            ("DefaultMage", "DefaultGolem", 1, false),
            ("DefaultWarrior", "DefaultDragon", 1, false),
            ("DefaultMage", "DefaultDragon", 1, false),
            ("DefaultWarrior", "DefaultOrc", 2, true),
            ("DefaultMage", "DefaultOrc", 2, true),
            ("DefaultWarrior", "DefaultGolem", 2, false),
            ("DefaultMage", "DefaultGolem", 2, false),
            ("DefaultWarrior", "DefaultDragon", 2, false),
            ("DefaultMage", "DefaultDragon", 2, false),
            ("DefaultWarrior", "DefaultOrc", 3, true),
            ("DefaultMage", "DefaultOrc", 3, true),
            ("DefaultWarrior", "DefaultGolem", 3, false),
            ("DefaultMage", "DefaultGolem", 3, false),
            ("DefaultWarrior", "DefaultDragon", 3, false),
            ("DefaultMage", "DefaultDragon", 3, false),
            ("DefaultWarrior", "DefaultOrc", 4, true),
            ("DefaultMage", "DefaultOrc", 4, true),
            ("DefaultWarrior", "DefaultGolem", 4, false),
            ("DefaultMage", "DefaultGolem", 4, false),
            ("DefaultWarrior", "DefaultDragon", 4, false),
            ("DefaultMage", "DefaultDragon", 4, false),
            ("DefaultWarrior", "DefaultOrc", 5, true),
            ("DefaultMage", "DefaultOrc", 5, true),
            ("DefaultWarrior", "DefaultGolem", 5, true),
            ("DefaultMage", "DefaultGolem", 5, true),
            ("DefaultWarrior", "DefaultDragon", 5, false),
            ("DefaultMage", "DefaultDragon", 5, false)
        };

        [Test]
        public IEnumerator TestWithSingleEnemy([ValueSource("battles")] (string unit, string enemy,int level, bool unitWin) battle)
        {
            UnitData unitData = LoadUnit(battle.unit);
            float unitHealth = unitData.health;
            EnemyData enemyData = LoadEnemy(battle.enemy);
            float enemyHealth = enemyData.health;
            float timer = 0;
            while (unitHealth > 0 && enemyHealth > 0)
            {

                if (timer % unitData.attackSpeed == 0)
                {
                    enemyHealth -= Mathf.Max(unitData.GetAttack(battle.level) - enemyData.defense, 0);
                }

                if (timer % enemyData.attackSpeed == 0 && enemyHealth > 0)
                {
                    unitHealth -= Mathf.Max(enemyData.attack - unitData.GetDefense(battle.level), 0);
                }

                timer += 0.5f;
                yield return null;
            }

            Assert.AreEqual(battle.unitWin, unitHealth > 0, "The unit was defeated but should have won.");
        }
    }
}