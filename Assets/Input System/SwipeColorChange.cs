using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeColorChange : MonoBehaviour
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

    void Start()
    {
        countdownSystem = FindObjectOfType<CountDownSystem>();

        canvasbad.SetActive(false);
        canvasgood.SetActive(false);
    }

    void Update()
    {
        DetectSwipe();
        RegisterChildren();
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
                                HandleSwipe(true);
                            }
                        }
                        else
                        {
                            if (swipeDirection.y < 0)
                            {
                                HandleSwipe(false);
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

        Debug.Log("Total children registered: " + childObjects.Count);
    }

    void HandleSwipe(bool isRightSwipe)
    {
        bool hasBadTag = false;
        foreach (var child in childObjects)
        {
            if (child != null && child.CompareTag("Bad"))
            {
                hasBadTag = true;
                break;
            }
        }

        if (isRightSwipe)
        {
            if (hasBadTag)
            {
                ChangeColor(rightSwipeBadColor);
                countdownSystem.WrongChoice();
                ScoreManager.instance.SubtractScore(10);
                ShowCanvas(canvasbad);
            }
            else
            {
                ChangeColor(rightSwipeSafeColor);
                countdownSystem.CorrectChoice();
                ScoreManager.instance.AddScore(20);
                ShowCanvas(canvasgood);
            }
        }
        else // Down swipe
        {
            if (hasBadTag)
            {
                ChangeColor(downSwipeBadColor);
                countdownSystem.CorrectChoice();
                ScoreManager.instance.AddScore(20);
                ShowCanvas(canvasgood);
            }
            else
            {
                ChangeColor(downSwipeSafeColor);
                countdownSystem.WrongChoice();
                ScoreManager.instance.SubtractScore(10);
                ShowCanvas(canvasbad);
            }
        }

        RemoveAllChildren();
    }

    void ShowCanvas(GameObject canvas)
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

    void RemoveAllChildren()
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
