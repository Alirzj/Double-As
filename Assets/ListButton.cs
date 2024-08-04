using UnityEngine;
using UnityEngine.SceneManagement;

public class PageController : MonoBehaviour
{
    public GameObject pg1;
    public GameObject pg2;
    public GameObject pg3;
    public GameObject nextButton;
    public GameObject prevButton;
    public GameObject pg3BackButton;
    public AudioSource audioClick;
    public GameObject insTitle;

    private int currentPage = 1;

    void Start()
    {
        prevButton.SetActive(false);
        pg3BackButton.SetActive(false);
        insTitle.SetActive(false);
        ShowPage(currentPage);
    }

    void Update()
    {
    }

    public void NextPage()
    {
        audioClick.Play();
        currentPage++;
        UpdatePageButtons();
        ShowPage(currentPage);
    }

    public void PrevPage()
    {
        audioClick.Play();
        currentPage--;
        UpdatePageButtons();
        ShowPage(currentPage);
    }

    public void PG3Button()
    {
        audioClick.Play();
        // Handle any specific logic for the PG3 button if needed
    }

    public void ExitButton()
    {
        audioClick.Play();
        SceneManager.LoadScene("Ali 1");
    }

    private void UpdatePageButtons()
    {
        // Determine if prevButton and nextButton should be active
        prevButton.SetActive(currentPage > 1);
        nextButton.SetActive(currentPage < 3);

        // Special handling for page 3 buttons
        pg3BackButton.SetActive(currentPage == 3);
    }

    private void ShowPage(int page)
    {
        // Hide all pages
        pg1.SetActive(false);
        pg2.SetActive(false);
        pg3.SetActive(false);
        insTitle.SetActive(false); // Deactivate insTitle by default

        // Show the selected page and activate insTitle on page 3
        switch (page)
        {
            case 1:
                pg1.SetActive(true);
                break;
            case 2:
                pg2.SetActive(true);
                break;
            case 3:
                pg3.SetActive(true);
                insTitle.SetActive(true);
                break;
        }
    }
}

