using UnityEngine;
using RTS;

public class BasicMageSpawner : BaseSpawner
{
    [SerializeField] private UnitData unitData;

    private void OnEnable()
    {
        MessageQueueManager.Instance.AddListener<DefaultMageSpawnMessage>(OnBasicMageSpawned);
    }

    private void OnDisable()
    {
        MessageQueueManager.Instance.RemoveListener<DefaultMageSpawnMessage>(OnBasicMageSpawned);
    }

    private void OnBasicMageSpawned(DefaultMageSpawnMessage message)
    {
        GameObject mage = SpawnObject();
        mage.SetLayerMaskToAllChildren("Unit");
        UnitComponentNavMesh unit = mage.GetComponent<UnitComponentNavMesh>();
        if (unit == null)
        {
            unit = mage.AddComponent<UnitComponentNavMesh>();
        }
        unit.CopyData(unitData);
    }
}
