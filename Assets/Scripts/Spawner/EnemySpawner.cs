using RTS;
using UnityEngine;

public class EnemySpawner : BaseSpawner
{
    [SerializeField] private EnemyData _enemyData;

    private void OnEnable()
    {
        switch (_enemyData.Type)
        {
            case EnemyType.Orc:
                MessageQueueManager.Instance.AddListener<DefaultOrcSpawnMessage>(OnEnemySpawned);
                break;
            case EnemyType.Golem:
                MessageQueueManager.Instance.AddListener<DefaultGolemSpawnMessage>(OnEnemySpawned);
                break;
            case EnemyType.Dragon:
                MessageQueueManager.Instance.AddListener<DefaultDragonSpawnMessage>(OnEnemySpawned);
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        switch (_enemyData.Type)
        {
            case EnemyType.Orc:
                MessageQueueManager.Instance.RemoveListener<DefaultOrcSpawnMessage>(OnEnemySpawned);
                break;
            case EnemyType.Golem:
                MessageQueueManager.Instance.RemoveListener<DefaultGolemSpawnMessage>(OnEnemySpawned);
                break;
            case EnemyType.Dragon:
                MessageQueueManager.Instance.RemoveListener<DefaultDragonSpawnMessage>(OnEnemySpawned);
                break;
            default:
                break;
        }
    }

    private void OnEnemySpawned(BaseEnemySpawnMessage message)
    {
        GameObject enemyObject = SpawnObject();
        enemyObject.SetLayerMaskToAllChildren("Unit");

        EnemyComponent enemyComponent = enemyObject.GetComponent<EnemyComponent>();
        if (enemyComponent == null)
        {
            enemyComponent = enemyObject.AddComponent<EnemyComponent>();
        }

        enemyComponent.CopyData(_enemyData, message.SpawnPoint);
    }
}
