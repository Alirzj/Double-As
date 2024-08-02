using UnityEngine;

public class BagCheckTest : MonoBehaviour
{
    public CountDownSystem countdownTimer; // Reference to the CountdownTimer component


    void Start()
    {
        // Ensure the CountdownTimer component is assigned in the Inspector
        if (countdownTimer == null)
        {
            countdownTimer = FindObjectOfType<CountDownSystem>();
        }
    }

    public void CheckBag(bool isCorrect)
    {
        if (isCorrect)
        {
            countdownTimer.CorrectChoice();
        }
        else
        {
            countdownTimer.WrongChoice();
        }
    }
}