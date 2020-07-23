using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class C_Gate : MonoBehaviour
{
    public int targetLevel;
    public GameObject mainCharacter;
    public GameObject crosshair;
    public GameObject cam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        DontDestroyOnLoad(mainCharacter);
        DontDestroyOnLoad(crosshair);
        DontDestroyOnLoad(cam);
        SceneManager.LoadScene(targetLevel);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.MoveGameObjectToScene(mainCharacter,scene);
        SceneManager.MoveGameObjectToScene(crosshair, scene);
        SceneManager.MoveGameObjectToScene(cam, scene);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
