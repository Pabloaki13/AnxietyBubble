using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the next scene (e.g., "GameScene")
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quit Game");
        Application.Quit();
    }
}