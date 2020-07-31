using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class C_MainMenu : MonoBehaviour
{
    private const int MainMenu = 0;
    private const int Game = 1;

    public void NewGame()
    {
        SceneManager.LoadScene(Game);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
