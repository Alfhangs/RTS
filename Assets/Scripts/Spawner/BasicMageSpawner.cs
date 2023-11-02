using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMageSpawner : BaseSpawner
{
    [SerializeField] private UnitData unitData;
    private void OnEnable()
    {
        MessageQueueManager.Instance.AddListener<BasicMageSpawnMessage>(OnBasicMageSpawned);
    }
    private void OnDisable()
    {
        MessageQueueManager.Instance.RemoveListener<BasicMageSpawnMessage>(OnBasicMageSpawned);
    }
    private void OnBasicMageSpawned(BasicMageSpawnMessage message)
    {
        GameObject mage = SpawnObject();
        //mage.SetLayerMaskToAllChildren("Unit");
        UnitComponent unit = mage.GetComponent<UnitComponent>();
        if (unit == null)
        {
            unit = mage.AddComponent<UnitComponent>();
        }
        unit.CopyData(unitData);
    }
}
