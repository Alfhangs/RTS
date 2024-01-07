using UnityEngine;
using Configuration;
using UnityEngine.AI;
using RTS.Enemy;

namespace Level
{
    public class LevelComponent : MonoBehaviour
    {
        [SerializeField] private LevelData levelData;
        [SerializeField] private GameObject plane;

        private float distanceBetweenEnemies = 3.0f;

        private void Start()
        {
            if (levelData == null || plane == null)
            {
                Debug.LogError("Missing LevelData or Plane");
                return;
            }

            // The plane game object always have a Collier component preset (unless manually remove)
            Collider planeCollider = plane.GetComponent<Collider>();
            // The size of the plane is the boundaries of the collider
            Vector3 planeSize = planeCollider.bounds.size;
            // Settings the start position to the upper left corner sinde the origin (0, 0) is in the middle of the screen
            Vector3 startPosition = new Vector3(-planeSize.x / 2, 0, planeSize.z / 2);

            // The distance of each instantiated prefab is the size of the slot in the map grid
            float offsetX = planeSize.x / levelData.Columns - 1;
            float offsetZ = planeSize.z / levelData.Rows - 1;

            Initialize(startPosition, offsetX, offsetZ);
            SpawnEnemyGroups();
        }

        private void Initialize(Vector3 start, float offsetX, float offsetZ)
        {
            foreach (LevelSlot slot in levelData.Slots)
            {
                // For each slot we try to find the level item based on the type, if it is null we move to the next one
                LevelItem levelItem = levelData.Configuration.FindByType(slot.ItemType);
                if (levelItem == null)
                {
                    continue;
                }

                // Calculates the X and Z based on the start position, coodinates and offset
                // Note that the values are added to the X because we are moving from left to right horiontally
                // and the values are subtracted from the Z because we are moving from top to bottom vertically
                float x = start.x + (slot.Coordinates.y * offsetX) + offsetX / 2;
                float z = start.z - (slot.Coordinates.x * offsetZ) - offsetZ / 2;
                // The Y is not used since we are instantiating the prefab based on the X and Z position of the grid 
                Vector3 position = new Vector3(x, 0, z);

                // Instantiates the prefab at the desired position with the default rotation (Quaternion.identity)
                // and set the game object with this script as the parent of the new game object
                GameObject item = Instantiate(levelItem.Prefab, position, Quaternion.identity, transform);

                switch (levelItem.CollisionType)
                {
                    case LevelItemCollisionType.Rigidbody:
                        item.AddComponent<BoxCollider>();
                        break;
                    case LevelItemCollisionType.NavMesh:
                        item.AddComponent<NavMeshObstacle>();
                        break;
                    case LevelItemCollisionType.None:
                    default:
                        break;
                }
            }
        }

        private void SpawnEnemyGroups()
        {
            foreach (EnemyGroupConfiguration enemyGroup in levelData.EnemyGroups)
            {
                SpawnEnemyGroup(enemyGroup);
            }
        }

        private void SpawnEnemyGroup(EnemyGroupConfiguration enemyGroup)
        {
            int rows = Mathf.RoundToInt(Mathf.Sqrt(enemyGroup.data.Enemies.Count));
            int counter = 0;

            for (int i = 0; i < enemyGroup.data.Enemies.Count; i++)
            {
                if (i > 0 && (i % rows) == 0)
                {
                    counter++;
                }

                float offsetX = (i % rows) * distanceBetweenEnemies;
                float offsetZ = counter * distanceBetweenEnemies;

                Vector3 offset = new Vector3(offsetX, 0, offsetZ);
                Vector3 spawnPoint = enemyGroup.position + offset;

                SpawnEnemy(enemyGroup.data.Enemies[i].Type, spawnPoint);
            }
        }

        private void SpawnEnemy(EnemyType enemyType, Vector3 spawnPoint)
        {
            switch (enemyType)
            {
                case EnemyType.Orc:
                    MessageQueueManager.Instance.SendMessage(new DefaultOrcSpawnMessage { SpawnPoint = spawnPoint });
                    break;
                case EnemyType.Golem:
                    MessageQueueManager.Instance.SendMessage(new DefaultGolemSpawnMessage { SpawnPoint = spawnPoint });
                    break;
                case EnemyType.Dragon:
                    MessageQueueManager.Instance.SendMessage(new DefaultDragonSpawnMessage { SpawnPoint = spawnPoint });
                    break;
                default:
                    break;
            }
        }
    }
}