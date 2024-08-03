using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagDestroy1 : MonoBehaviour
{
    public GameObject bag1;
    public GameObject bag2;
    public float conveyorSpeed = 2.0f;
    public Transform spawnPoint;
    public Transform teleportPoint;
    public Collider2D endCollider;
    public Sprite[] bagSprites;
    public GameObject currentBag;
    public bool bag1MoveRight = true;
    public bool bag2MoveRight = true;
    private void Start()
    {
        currentBag = bag1;
    }
    private void Update()
    {
        MoveBag();
        UpdateCurrentBag();
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
}
