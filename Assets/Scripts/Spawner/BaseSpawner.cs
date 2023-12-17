using UnityEngine;

[RequireComponent(typeof(ObjectPoolComponent))]
public class BaseSpawner : MonoBehaviour
{
    private ObjectPoolComponent objectPoolComponent;

    private void Awake()
    {
        objectPoolComponent = GetComponent<ObjectPoolComponent>();
    }

    public GameObject SpawnObject()
    {
        return objectPoolComponent.GetObject();
    }
}
