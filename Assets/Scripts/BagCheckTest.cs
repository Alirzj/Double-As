using UnityEngine;

public class BagCheckTest : MonoBehaviour
{
    public CountdownTimer countdownTimer; // Reference to the CountdownTimer component


    void Start()
    {
        // Ensure the CountdownTimer component is assigned in the Inspector
        if (countdownTimer == null)
        {
            countdownTimer = FindObjectOfType<CountdownTimer>();
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