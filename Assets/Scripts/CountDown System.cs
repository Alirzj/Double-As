using UnityEngine;
using TMPro;
using System.Collections;

public class CountDownSystem : MonoBehaviour
{
    public float startTimeMinutes = 1f; // The time to countdown from in minutes
    public TextMeshProUGUI preCountdownText; // TextMeshProUGUI for the 3, 2, 1 countdown
    public TextMeshProUGUI countdownText; // TextMeshProUGUI for the actual timer
    public GameObject gameOverCanvas; // Canvas to activate when countdown ends
    public GameObject highScoreUI; // UI element for displaying high score
    public AudioSource tickAudio; // AudioSource for the ticking sound
    public GameObject TimesUPtext;
    public AudioSource Alarm;

    private NewSwipe swipe;

    public Animator Ogu;
    public Animator Tappy;
    public GameObject OguObject;
    public GameObject TappyObject;

    public GameObject rightChecker;
    public GameObject spawnManager;

    public BagDestroy1 bagDestroy;
    private bool gameOver = false;
    private float countdownTime;
    private bool gameActive = false;

    void Start()
    {
        StartCoroutine(StartCountdown());
        swipe = FindObjectOfType<NewSwipe>();

    }

    IEnumerator StartCountdown()
    {
        // Pause the game
        Time.timeScale = 0f;

        preCountdownText.gameObject.SetActive(true);
        countdownText.gameObject.SetActive(false);

        preCountdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        preCountdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        preCountdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        preCountdownText.text = "Go!!";
        yield return new WaitForSecondsRealtime(1f);

        preCountdownText.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(true);

        // Resume the game
        Time.timeScale = 1f;

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

        if (countdownTime <= 10 && !tickAudio.isPlaying)
        {
            tickAudio.Play();
        }
        else if (countdownTime > 10 && tickAudio.isPlaying)
        {
            tickAudio.Stop();
        }
    }

    void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        rightChecker.SetActive(false);
        spawnManager.SetActive(false);
        //swipe.ToggleSwipeDetection(false);
        bagDestroy.conveyorSpeed = 0;

        tickAudio.volume = 0f;
        Alarm.Play();
        gameOver = true;

        // Display TimesUPtext for 1 second
        TimesUPtext.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        TimesUPtext.SetActive(false);
        //playMasscotAnimation();
        TappyObject.SetActive(true);
        OguObject.SetActive(true);

        Ogu.SetTrigger("Play");
        Tappy.SetTrigger("Play");

        // Continue with the rest of the GameOver logic
        ScoreManager.instance.CheckAndSaveHighestScore(gameOver);
        countdownText.text = "0:00";
        gameOverCanvas.SetActive(true);
        highScoreUI.SetActive(true);

        // Finally, stop the game time
        //Time.timeScale = 0f; // Stop the game
    }

    public void CorrectChoice()
    {
        countdownTime += 10f; // Gain 10 seconds for a correct choice
        UpdateCountdownText();
    }

    public void WrongChoice()
    {
        countdownTime -= 20f; // Lose 10 seconds for a wrong choice
        UpdateCountdownText();

        if (countdownTime <= 0)
        {
            countdownTime = 0;
            gameActive = false;
            GameOver();
        }
    }

    //public void playMasscotAnimation()
    //{
    //    TappyObject.SetActive(true);
    //    OguObject.SetActive(true);

    //    Ogu.SetTrigger("Play");
    //    Tappy.SetTrigger("Play");
    //}
}
