using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public AudioSource buttons;

    public void PlayGame()
    {
        buttons.Play();
        // Load the game scene (replace "GameScene" with your actual game scene name)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }

    public void OpenOptions()
    {
        buttons.Play();
        // Show options menu and hide main menu
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenCredits()
    {
        buttons.Play();
        // Show credits menu and hide main menu
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        buttons.Play();
        // Show main menu and hide options/credits menu
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void ExitGame()
    {
        buttons.Play();
        // Quit the application
        Application.Quit();
        // Note: This will not work in the editor. To test in the editor, you can use:
        // UnityEditor.EditorApplication.isPlaying = false; // Uncomment this line if you are in the editor
    }

    public void PlayButtonAudio()
    {
        buttons.Play();
    }
}
