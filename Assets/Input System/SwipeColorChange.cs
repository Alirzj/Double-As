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

    private List<Transform> childObjects = new List<Transform>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            childObjects.Add(child);
        }
    }

    void Update()
    {
        DetectSwipe();
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
                                HandleSwipe(rightSwipeBadColor, rightSwipeSafeColor);
                            }
                        }
                        else
                        {
                            if (swipeDirection.y < 0)
                            {
                                HandleSwipe(downSwipeBadColor, downSwipeSafeColor);
                            }
                        }
                    }
                    isSwipe = false;
                    break;
            }
        }
    }

    void HandleSwipe(Color badColor, Color safeColor)
    {
        bool hasBadTag = false;
        foreach (var child in childObjects)
        {
            if (child.CompareTag("Bad"))
            {
                hasBadTag = true;
                break;
            }
        }

        if (hasBadTag)
        {
            ChangeColor(badColor);
        }
        else
        {
            ChangeColor(safeColor);
        }
    }

    void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}
