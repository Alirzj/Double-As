using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightCheck : MonoBehaviour
{
    public bool bagOut;
    private SwipeColorChange swipe;
    private ScoreManager scoreManager;
    private CountDownSystem countDownSystem;
    public GameObject canvasbad;
    public GameObject canvasgood;

    //private leftCheck checkIn;

    // Start is called before the first frame update
    void Start()
    {
        //checkIn = FindObjectOfType<leftCheck>();
        swipe = FindObjectOfType<SwipeColorChange>();
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

            bagOut = true;

            if (swipe.hasBadTag == true)
            {
                countDownSystem.WrongChoice();
                scoreManager.SubtractScore(10);
                swipe.ShowCanvas(canvasbad);
                swipe.RemoveAllChildren();
            }
            else if (swipe.hasBadTag == false)
            {
                countDownSystem.CorrectChoice();
                scoreManager.AddScore(20);
                swipe.ShowCanvas(canvasgood);
                swipe.RemoveAllChildren();
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
