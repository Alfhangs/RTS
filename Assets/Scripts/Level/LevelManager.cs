using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject miniMapCameraPrefab;
    [SerializeField] private Object sceneToLoad;

    private void Start()
    {
        // Instantiates the mini map on the current scene
        Instantiate(miniMapCameraPrefab);

        // Loads the "GameUI" scene additively on top of the current scene
        SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Additive);
    }
}
