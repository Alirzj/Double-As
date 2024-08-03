using UnityEngine;

public class MainMenu1 : MonoBehaviour
{
    public AudioSource buttons;

    public void Restart()
    {
        buttons.Play();
        // Load the game scene (replace "GameScene" with your actual game scene name)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ali 1");
    }

    public void ExitToHome()
    {
        buttons.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void PlayButtonAudio()
    {
        buttons.Play();
    }
}
