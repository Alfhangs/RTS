using RTS;
using RTS.Level;
using UnityEngine;

public class BasicWarriorSpawner : BaseSpawner
{
    [SerializeField] private UnitData unitData;

    private void OnEnable()
    {
        MessageQueueManager.Instance.AddListener<DefaultWarriorSpawnMessage>(OnBasicWarriorSpawned);
    }

    private void OnDisable()
    {
        MessageQueueManager.Instance.RemoveListener<DefaultWarriorSpawnMessage>(OnBasicWarriorSpawned);
    }

    private void OnBasicWarriorSpawned(DefaultWarriorSpawnMessage message)
    {
        GameObject warrior = SpawnObject();
        warrior.SetLayerMaskToAllChildren("Unit");
        UnitComponentNavMesh unit = warrior.GetComponent<UnitComponentNavMesh>();
        if (unit == null)
        {
            unit = warrior.AddComponent<UnitComponentNavMesh>();
        }
        unit.CopyData(unitData);
        LevelManager.Instance.Units.Add(warrior);
    }
}
