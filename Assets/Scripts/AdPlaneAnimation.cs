using UnityEngine;
using System.Collections; // Required for IEnumerator

public class AdPlaneAnimation : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component

    void Start()
    {
        StartCoroutine(TriggerAnimationRandomly());
    }

    IEnumerator TriggerAnimationRandomly()
    {
        while (true)
        {
            // Wait for a random time between 10 to 20 seconds
            float waitTime = Random.Range(10f, 20f);
            yield return new WaitForSeconds(waitTime);

            // Trigger the animation
            animator.SetTrigger("Start");
        }
    }
}