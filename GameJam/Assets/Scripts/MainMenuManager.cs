using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject CanvasMainMenu;
    public GameObject DialogBackground;
    public GameObject Dialog;

    public void PlayGame()
    {
        DialogBackground.GetComponent<Image>().enabled = true;
        Dialog.GetComponent<Dialogs>().type();
        CanvasMainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quit Game");
        Application.Quit();
    }
}