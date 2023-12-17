using UnityEditor;
using UnityEngine;

public static class EnemyDebugger
{
    [MenuItem("RTS/Debug/Enemy/Spawn Orc", priority = 0)]
    private static void SpawnOrc()
    {
        MessageQueueManager.Instance.SendMessage(new DefaultOrcSpawnMessage() { SpawnPoint = new Vector3(-6, 0, 0) });
    }

    [MenuItem("RTS/Debug/Enemy/Spawn Golem", priority = 1)]
    private static void SpawnGolem()
    {
        MessageQueueManager.Instance.SendMessage(new DefaultGolemSpawnMessage() { SpawnPoint = new Vector3(6, 0, 0) });
    }

    [MenuItem("RTS/Debug/Enemy/Spawn Red Dragon", priority = 2)]
    private static void SpawnDragon()
    {
        MessageQueueManager.Instance.SendMessage(new DefaultDragonSpawnMessage() { SpawnPoint = new Vector3(0, 0, 6) });
    }

    [MenuItem("RTS/Debug/Enemy/Spawn All", priority = 3)]
    private static void SpawnAll()
    {
        SpawnOrc();
        SpawnGolem();
        SpawnDragon();
    }
}