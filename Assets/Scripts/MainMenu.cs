using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;

    public void PlayGame()
    {
        // Load the game scene (replace "GameScene" with your actual game scene name)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ali");
    }

    public void OpenOptions()
    {
        // Show options menu and hide main menu
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenCredits()
    {
        // Show credits menu and hide main menu
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        // Show main menu and hide options/credits menu
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
        // Note: This will not work in the editor. To test in the editor, you can use:
        // UnityEditor.EditorApplication.isPlaying = false; // Uncomment this line if you are in the editor
    }
}
