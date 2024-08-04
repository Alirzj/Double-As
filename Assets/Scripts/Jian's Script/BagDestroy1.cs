using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagDestroy1 : MonoBehaviour
{
    public GameObject bag1;
    public GameObject bag2;
    public float conveyorSpeed;
    public Transform spawnPoint;
    public Transform teleportPoint;
    public Collider2D endCollider;
    public Sprite[] bagSprites;
    public GameObject currentBag;
    public bool bag1MoveRight = true;
    public bool bag2MoveRight = true;
    private ScoreManager scoreManager;

    private void Start()
    {
        currentBag = bag1;
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void Update()
    {
        MoveBag();
        UpdateCurrentBag();
        ScoreToSpeed();
    }
    public void MoveBag()
    {
        if (bag1MoveRight)
        {
            bag1.transform.Translate(Vector2.right * conveyorSpeed * Time.deltaTime);
        }
        else
        {
            bag1.transform.Translate(Vector2.down * conveyorSpeed * Time.deltaTime);
        }
        if (bag2MoveRight)
        {
            bag2.transform.Translate(Vector2.right * conveyorSpeed * Time.deltaTime);
        }
        else
        {
            bag2.transform.Translate(Vector2.down * conveyorSpeed * Time.deltaTime);
        }
    }
    private void UpdateCurrentBag()
    {
        currentBag = (bag1.transform.position.x > bag2.transform.position.x) ? bag1 : bag2;
    }
    public void SwipeBagDown()
    {
        if (currentBag == bag1)
        {
            TeleportAndMoveDown(bag1);
        }
        else if (currentBag == bag2)
        {
            TeleportAndMoveDown(bag2);
        }
    }
    private void TeleportAndMoveDown(GameObject bag)
    {
        bag.transform.position = teleportPoint.position;
        if (bag == bag1)
        {
            bag1MoveRight = false;
        }
        else if (bag == bag2)
        {
            bag2MoveRight = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == bag1 || collision.gameObject == bag2)
        {
            RespawnBag(collision.gameObject);
        }
    }
    public void RespawnBag(GameObject bag)
    {
        bag.transform.position = spawnPoint.position;
        if (bag == bag1)
        {
            bag1MoveRight = true;
        }
        else if (bag == bag2)
        {
            bag2MoveRight = true;
        }
        SpriteRenderer spriteRenderer = bag.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && bagSprites.Length > 0)
        {
            int randomIndex = Random.Range(0, bagSprites.Length);
            spriteRenderer.sprite = bagSprites[randomIndex];
        }
    }
    public GameObject GetCurrentBag()
    {
        return currentBag;
    }

    private void ScoreToSpeed()
    {
        // Calculate the number of 50-point increments in the current score
        int increments = scoreManager.currentScore / 50;

        // Calculate the new conveyor speed based on the score
        float newSpeed = 3.0f + (increments * 0.5f);

        // Clamp the speed to a maximum value of 6.0f
        conveyorSpeed = Mathf.Min(newSpeed, 6.0f);
    }



}
