using UnityEngine;
using TMPro;
using System.Collections;

public class CountDownSystem : MonoBehaviour
{
    public float startTimeMinutes = 1f; // The time to countdown from in minutes
    public TextMeshProUGUI preCountdownText; // TextMeshProUGUI for the 3, 2, 1 countdown
    public TextMeshProUGUI countdownText; // TextMeshProUGUI for the actual timer
    public GameObject gameOverCanvas; // Canvas to activate when countdown ends

    private float countdownTime;
    private bool gameActive = false;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        preCountdownText.gameObject.SetActive(true);
        countdownText.gameObject.SetActive(false);

        preCountdownText.text = "3";
        yield return new WaitForSeconds(1f);
        preCountdownText.text = "2";
        yield return new WaitForSeconds(1f);
        preCountdownText.text = "1";
        yield return new WaitForSeconds(1f);

        preCountdownText.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(true);

        countdownTime = startTimeMinutes * 60; // Convert minutes to seconds
        gameActive = true;
        UpdateCountdownText();
    }

    void Update()
    {
        if (gameActive)
        {
            countdownTime -= Time.deltaTime;

            if (countdownTime <= 0)
            {
                countdownTime = 0;
                gameActive = false;
                GameOver();
            }

            UpdateCountdownText();
        }
    }

    void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60F);
        int seconds = Mathf.FloorToInt(countdownTime % 60F);
        countdownText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        countdownText.text = "0:00";
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f; // Stop the game
    }

    public void CorrectChoice()
    {
        countdownTime += 10f;
        UpdateCountdownText();
    }

    public void WrongChoice()
    {
        countdownTime -= 10f;
        UpdateCountdownText();

        if (countdownTime <= 0)
        {
            countdownTime = 0;
            gameActive = false;
            GameOver();
        }
    }
}
