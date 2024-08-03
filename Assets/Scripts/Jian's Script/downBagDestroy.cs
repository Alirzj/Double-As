using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downBagDestroy : MonoBehaviour
{
    public GameObject bag1;
    public GameObject bag2;
    public BagDestroy1 conveyorBeltScript;  // Reference to the ConveyorBelt script
    public Transform spawnPoint;
    public Sprite[] bagSprites;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == bag1 || collision.gameObject == bag2)
        {
            RespawnBag(collision.gameObject);
        }
    }

    private void RespawnBag(GameObject bag)
    {
        bag.transform.position = spawnPoint.position;
        if (bag == bag1)
        {
            conveyorBeltScript.bag1MoveRight = true;  // Resume moving right
        }
        else if (bag == bag2)
        {
            conveyorBeltScript.bag2MoveRight = true;  // Resume moving right
        }
        SpriteRenderer spriteRenderer = bag.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && bagSprites.Length > 0)
        {
            int randomIndex = Random.Range(0, bagSprites.Length);
            spriteRenderer.sprite = bagSprites[randomIndex];
        }

        // Call the MoveBag method to resume movement
        conveyorBeltScript.MoveBag();
    }
}
