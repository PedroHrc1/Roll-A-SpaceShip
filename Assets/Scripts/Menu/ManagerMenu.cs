using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenu : MonoBehaviour
{
    // Function to load the game scene
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);  // Loads scene 1 (index or name)
    }

    // Function to quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
        // Simulate quitting in the editor by logging a message
        Debug.Log("QuitGame() called - Game would quit if running in a build.");
        #else
        // This will quit the game in a built executable
        Application.Quit();
        #endif
    }
}
