using UnityEngine;
using Configuration;
using UnityEngine.AI;

namespace Level
{
    public class LevelComponent : MonoBehaviour
    {
        [SerializeField]
        private LevelData levelData;

        [SerializeField]
        private GameObject plane;

        private void Start()
        {
            if (levelData == null || plane == null)
            {
                Debug.LogError("Missing LevelData or Plane");
                return;
            }

            Collider planeCollider = plane.GetComponent<Collider>();
            Vector3 planeSize = planeCollider.bounds.size;

            Vector3 startPosition = new Vector3(-planeSize.x / 2, 0,
                                                 planeSize.z / 2);

            float offsetX = planeSize.x / levelData.Columns - 1;
            float offsetZ = planeSize.z / levelData.Rows - 1;

            Initialize(startPosition, offsetX, offsetZ);
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
    }
}