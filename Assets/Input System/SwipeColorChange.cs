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
    private List<Transform> childObjects = new List<Transform>();
    private CountDownSystem countdownSystem;

    void Start()
    {
        foreach (Transform child in transform)
        {
            childObjects.Add(child);
        }
        countdownSystem = FindObjectOfType<CountDownSystem>();

        canvasbad.SetActive(false);
        canvasgood.SetActive(false);
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

    void HandleSwipe(bool isRightSwipe)
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
}
