using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class leftCheck : MonoBehaviour
{
    public bool bagIn;
    private GameObject detectedBag;
    private NewSwipe swipe;

    // Start is called before the first frame update
    void Start()
    {
        swipe = FindObjectOfType<NewSwipe>();
        swipe.ToggleSwipeDetection(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Any logic that should be checked every frame
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bag")
        {
            swipe.ToggleSwipeDetection(true);
            bagIn = true;
            detectedBag = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bag")
        {
            bagIn = false;
            //detectedBag = null;
        }
    }

    public GameObject GetDetectedBag()
    {
        return detectedBag;
    }
}
