using NUnit.Framework;
using System.Collections.Generic;

namespace RTS.Test
{
    public class EnemyDataTest : BaseTest
    {
        private static List<string> enemies = new List<string>()
        {
            "DefaultDragon",
            "DefaultGolem",
            "DefaultOrc"
        };
        [Test]
        public void TestWithEnemies([ValueSource("enemies")] string enemy)
        {
            EnemyData enemyData = LoadEnemy(enemy);

            Assert.IsNotNull(enemyData, $"EnemyData {enemy} not found.");

            Assert.IsTrue(enemyData.health > 0, "Health must be greater than 0.");
            Assert.IsTrue(enemyData.attack > 0, "Attack must be greater than 0.");
            Assert.IsTrue(enemyData.defense >= 0, "Defense must be equal or greater than 0.");
            Assert.IsTrue(enemyData.walkSpeed > 0, "WalkSpeed must be greater than 0.");
            Assert.IsNotNull(enemyData.selectedColor, "SelectedColor must not be null.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.animationStateAttack01), "AnimationStateAttack01 must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.animationStateAttack02), "AnimationStateAttack02 must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.animationStateDefense), "AnimationStateDefense must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.animationStateMove), "AnimationStateMove must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.animationStateIdle), "AnimationStateIdle must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.animationStateCollect), "AnimationStateCollect must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.animationStateDeath), "AnimationStateDeath must not be null or empty.");
            Assert.IsTrue(enemyData.attackRange >= 0, "AttackRange must be equal or greater than 0.");
            Assert.IsTrue(enemyData.colliderSize > 0, "ColliderSize must be greater than 0.");
        }
    }
}