using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWarriorSpawner : BaseSpawner
{
    [SerializeField] public UnitData _unitData;

    private void OnEnable()
    {
        MessageQueueManager.Instance.AddListener<IMessage>(OnBasicWarriorSpawned);
    }

    private void OnDisable()
    {
        MessageQueueManager.Instance.RemoveListener<IMessage>(OnBasicWarriorSpawned);
    }

    private void OnBasicWarriorSpawned(IMessage message)
    {
        GameObject warrior = SpawnObject();

        UnitComponent unit = warrior.GetComponent<UnitComponent>();
        if (unit == null)
        {
            unit = warrior.GetComponent<UnitComponent>();
        }
        unit.CopyData(_unitData);
        //unit.ID = _unitData.ToString();
        //unit.type = _unitData.Type;
        //unit.level = _unitData.Level;
        //unit.levelMultiplier = _unitData.LevelMultiplier;
        //unit.health = _unitData.Health;
        //unit.attack = _unitData.Attack;
        //unit.defense = _unitData.Defense;
        //unit.walkSpeed = _unitData.WalkSpeed;
        //unit.attackSpeed = _unitData.AttackSpeed;
    }

}
