using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolComponent : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;
    [SerializeField] private bool allowCreation;
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            gameObjects.Add(CreateItem(true));
        }
    }

    private GameObject CreateItem(bool active)
    {
        GameObject item = Instantiate(prefab);
        item.transform.SetParent(transform);
        item.SetActive(active);
        return item;
    }

    public GameObject GetObject()
    {
        foreach (GameObject item in gameObjects)
        {
            if (!item.activeSelf)
            {
                item.SetActive(true);
                return item;
            }
        }
        if (allowCreation)
        {
            GameObject item = CreateItem(true);
            gameObjects.Add(item);
            return item;
        }
        return null;
    }
}
