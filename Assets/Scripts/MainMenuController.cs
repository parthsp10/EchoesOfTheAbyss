using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Alpha");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game (will not close editor/WebGL).");
    }
}


