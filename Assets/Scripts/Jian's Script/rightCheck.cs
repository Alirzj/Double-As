using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightCheck : MonoBehaviour
{
    public bool bagOut;
    private NewSwipe swipe;
    private ScoreManager scoreManager;
    private CountDownSystem countDownSystem;
    public GameObject canvasbad;
    public GameObject canvasgood;
    public AudioSource correctAudio;
    public AudioSource wrongAudio;
    public Animator WrongCharacterAnimator;
    public Animator CorrectCharacterAnimator;

    //private leftCheck checkIn;

    // Start is called before the first frame update
    void Start()
    {
        //checkIn = FindObjectOfType<leftCheck>();
        swipe = FindObjectOfType<NewSwipe>();
        scoreManager = FindObjectOfType<ScoreManager>();
        countDownSystem = FindObjectOfType<CountDownSystem>();
        canvasbad.SetActive(false);
        canvasgood.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (checkIn.bagIn == true)
        //{
        //    bagOut = false;
        //}
        //else if (checkIn.bagIn == false)
        //{
        //    bagOut = true;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bag")
        {
            swipe.ToggleSwipeDetection(false);
            bagOut = true;

            if (swipe.hasBadTag == true)
            {
                WrongCharacterAnimator.SetTrigger("Start");
                countDownSystem.WrongChoice();
                scoreManager.SubtractScore(0);
                swipe.ShowCanvas(canvasbad);
                swipe.RemoveAllChildren();
                wrongAudio.Play();

            }
            else if (swipe.hasBadTag == false)
            {
                CorrectCharacterAnimator.SetTrigger("Start");
                countDownSystem.CorrectChoice();
                scoreManager.AddScore(20);
                swipe.ShowCanvas(canvasgood);
                swipe.RemoveAllChildren();
                correctAudio.Play();
                
            }
        }

        //if (swipe.hasBadTag == true)
        //{
        //    countDownSystem.WrongChoice();
        //    scoreManager.SubtractScore(10);
        //    swipe.ShowCanvas(canvasbad);
        //    swipe.RemoveAllChildren();
        //}

        //if (swipe.hasBadTag == false)
        //{
        //    countDownSystem.CorrectChoice();
        //    scoreManager.AddScore(20);
        //    swipe.ShowCanvas(canvasgood);
        //    swipe.RemoveAllChildren();
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bag")
        {
            bagOut = false;
        }
    }
}
