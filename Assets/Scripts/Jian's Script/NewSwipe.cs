using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSwipe : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private bool isSwipe;
    public Color rightSwipeBadColor = Color.red; // Color when swiped right and "Bad" tag is found
    public Color downSwipeBadColor = Color.green; // Color when swiped down and "Bad" tag is found
    public Color rightSwipeSafeColor = Color.green; // Color when swiped right and all tags are "Safe"
    public Color downSwipeSafeColor = Color.red; // Color when swiped down and all tags are "Safe"
    public GameObject canvasgood;
    public GameObject canvasbad;
    public List<Transform> childObjects = new List<Transform>();
    private HashSet<Transform> registeredChildren = new HashSet<Transform>();
    private CountDownSystem countdownSystem;
    public bool hasBadTag;
    public AudioSource correctAudio;
    public AudioSource wrongAudio;
    private leftCheck leftChecker;
    private GameObject bag;
    public Transform rightPosition;
    public Transform downPosition;
    private BagDestroy1 bagDestroy1;


    void Start()
    {
        countdownSystem = FindObjectOfType<CountDownSystem>();
        leftChecker = FindObjectOfType<leftCheck>();
        bagDestroy1 = FindObjectOfType<BagDestroy1>();


        canvasbad.SetActive(false);
        canvasgood.SetActive(false);
    }

    void Update()
    {
        DetectSwipe();
        RegisterChildren();
        BadTagCheck();
    }

    void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSwipe = true;
                    break;
                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    if (isSwipe)
                    {
                        Vector2 swipeDirection = endTouchPosition - startTouchPosition;
                        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                        {
                            if (swipeDirection.x > 0)
                            {
                                Debug.Log("Right Swipe Detected");
                                HandleSwipe(true);
                            }
                        }
                        else
                        {
                            if (swipeDirection.y < 0)
                            {
                                Debug.Log("Down Swipe Detected");
                                HandleSwipe(false);
                                bagDestroy1.SwipeBagDown();
                            }
                        }
                    }
                    isSwipe = false;
                    break;
            }
        }
    }


    void RegisterChildren()
    {
        foreach (Transform child in transform)
        {
            if (!registeredChildren.Contains(child))
            {
                childObjects.Add(child);
                registeredChildren.Add(child);
                Debug.Log("Added child: " + child.name);
            }
        }
    }

    void BadTagCheck()
    {
        hasBadTag = false;
        foreach (var child in childObjects)
        {
            if (child != null && child.CompareTag("Bad"))
            {
                hasBadTag = true;
                break;
            }
        }
    }

    void TeleportBag()
    {
        if (bag != null && rightPosition != null)
        {
            bag.transform.position = rightPosition.position;
            Debug.Log("Bag teleported to: " + rightPosition.position);
        }
    }

    void TeleportBagDown()
    {
        if (bag != null && downPosition != null)
        {
            bag.transform.position = downPosition.position;
            Debug.Log("Bag teleported to: " + downPosition.position);
        }
    }

    void HandleSwipe(bool isRightSwipe)
    {
        bag = leftChecker.GetDetectedBag();
    if (bag != null)
    {
        if (isRightSwipe)
        {
            TeleportBag();
        }
        else // Down swipe
        {
            TeleportBagDown();
            // Set a flag or component to move the bag down on the conveyor belt
            bag.GetComponent<BagMovement>().isMovingDown = true;
        }
    }
    }

    public void ShowCanvas(GameObject canvas)
    {
        canvas.SetActive(true);
        StartCoroutine(HideCanvasAfterDelay(canvas, 0.5f));
    }

    IEnumerator HideCanvasAfterDelay(GameObject canvas, float delay)
    {
        yield return new WaitForSeconds(delay);
        canvas.SetActive(false);
    }

    void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public void RemoveAllChildren()
    {
        foreach (Transform child in childObjects)
        {
            if (child != null)
            {
                registeredChildren.Remove(child);
                Destroy(child.gameObject);
            }
        }
        childObjects.Clear();
        Debug.Log("All children removed.");
    }
}
