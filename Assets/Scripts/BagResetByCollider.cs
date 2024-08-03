using UnityEngine;

public class BagAnimationActivator : MonoBehaviour
{
    public Animator bagAnimator;
    public Animator bagAnimator2; // Assign the Animator of the bag you want to activate

    private void Start()
    {
        bagAnimator2.SetTrigger("Start2");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bag"))
        {
            bagAnimator.SetTrigger("Start");
        }
    }
}
