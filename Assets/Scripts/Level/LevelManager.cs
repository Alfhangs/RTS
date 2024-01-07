using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject miniMapCameraPrefab;
    [SerializeField] private Object sceneToLoad;
    [SerializeField] private GameObject fog;

    public List<GameObject> Units { private set; get; }
    public static LevelManager Instance { private set; get; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        Units = new List<GameObject>();
    }

    private void Start()
    {
        if (miniMapCameraPrefab == null || fog == null)
        {
            Debug.LogError("Missing MiniMapCamera prefab or Fog game object");
            return;
        }

        // Enable the fog game object
        fog.SetActive(true);

        // Instantiates the mini map on the current scene
        Instantiate(miniMapCameraPrefab);

        // Loads the "GameUI" scene additively on top of the current scene
        SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Additive);
    }
}
