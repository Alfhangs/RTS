using RTS;
using UnityEngine;

public class BasicWarriorSpawner : BaseSpawner
{
    [SerializeField] private UnitData unitData;

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
        warrior.SetLayerMaskToAllChildren("Unit");
        UnitComponentNavMesh unit = warrior.GetComponent<UnitComponentNavMesh>();
        if (unit == null)
        {
            unit = warrior.GetComponent<UnitComponentNavMesh>();
        }
        unit.CopyData(unitData);
    }
}
