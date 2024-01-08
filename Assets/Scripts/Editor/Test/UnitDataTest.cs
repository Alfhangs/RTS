using NUnit.Framework;
using System.Collections.Generic;

namespace RTS.Test
{
    public class UnitDataTest : BaseTest
    {
        private static List<string> units = new List<string>()
        {
            "DefaultWarrior",
            "DefaultMage"
        };

        [Test]
        public void TestWithUnits([ValueSource("units")] string unit)
        {
            UnitData unitData = LoadUnit(unit);

            Assert.IsNotNull(unitData, $"UnitData {unit} not found.");


            Assert.IsTrue(unitData.health > 0, "Health must be greater than 0.");
            Assert.IsTrue(unitData.attack > 0, "Attack must be greater than 0.");
            Assert.IsTrue(unitData.defense >= 0, "Defense must be equal or greater than 0.");
            Assert.IsTrue(unitData.walkSpeed > 0, "WalkSpeed must be greater than 0.");
            Assert.IsNotNull(unitData.selectedColor, "SelectedColor must not be null.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.animationStateAttack01), "AnimationStateAttack01 must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.animationStateAttack02), "AnimationStateAttack02 must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.animationStateDefense), "AnimationStateDefense must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.animationStateMove), "AnimationStateMove must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.animationStateIdle), "AnimationStateIdle must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.animationStateCollect), "AnimationStateCollect must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.animationStateDeath), "AnimationStateDeath must not be null or empty.");
            Assert.IsTrue(unitData.attackRange >= 0, "AttackRange must be equal or greater than 0.");
            Assert.IsTrue(unitData.colliderSize > 0, "ColliderSize must be greater than 0.");

            Assert.IsTrue(unitData.level >= 0, "Level must be equal or greater than 0.");
            Assert.IsTrue(unitData.levelMultiplier > 0, "LevelMultiplier must be greater than 0.");
            Assert.IsFalse(unitData.actions == Configuration.ActionType.None, "Actions must be different than None.");
        }
    }
}