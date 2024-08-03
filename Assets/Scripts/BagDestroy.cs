using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject bag1;
    public GameObject bag2;
    public float conveyorSpeed = 2.0f;
    public Transform spawnPoint;
    public Collider2D endCollider;
    public Sprite[] bagSprites; // Array of sprites for the bags

    private void Update()
    {
        MoveBag(bag1);
        MoveBag(bag2);
    }

    private void MoveBag(GameObject bag)
    {
        bag.transform.Translate(Vector2.right * conveyorSpeed * Time.deltaTime);
    }

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

        // Change the sprite of the bag
        SpriteRenderer spriteRenderer = bag.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && bagSprites.Length > 0)
        {
            int randomIndex = Random.Range(0, bagSprites.Length);
            spriteRenderer.sprite = bagSprites[randomIndex];
        }
    }
}
