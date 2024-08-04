using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name;
    public GameObject Prefab;
    [Range(0f, 100f)] public float Chance = 100f;
    [HideInInspector] public double _weight;
}

public class Spawner3 : MonoBehaviour
{
    [SerializeField] private Item[] items;
    private double accumulatedWeights;
    private System.Random rand = new System.Random();
    private List<GameObject> spawnedItems = new List<GameObject>();
    private NewSwipe swipeColorChange;

    private void Awake()
    {
        CalculateWeights();
    }

    [Range(1, 10)] public int minObjectNum = 1;
    [Range(1, 10)] public int maxObjectNum = 5;

    public Vector2 spawnAreaSize = new Vector2(4f, 3f);
    public bool showSpawnPoints = true;
    private rightCheck checkOut;
    private leftCheck checkIn;
    private downCheck checkDownOut;
    private bool hasSpawned = false;

    void Start()
    {
        checkOut = FindObjectOfType<rightCheck>();
        checkIn = FindObjectOfType<leftCheck>();
        checkDownOut = FindObjectOfType<downCheck>();
        swipeColorChange = GetComponent<NewSwipe>();
    }

    void Update()
    {
        if (checkIn != null && checkIn.bagIn && !hasSpawned)
        {
            int objectNum = Random.Range(minObjectNum, maxObjectNum + 1); // Randomize objectNum within the specified range
            for (int i = 0; i < objectNum; i++)
            {
                SpawnRandomItems(new Vector2(Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f), Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f)));
            }
            hasSpawned = true;
        }

        if (checkOut.bagOut || checkDownOut.bagDownOut)
        {
            hasSpawned = false; // Reset the flag to allow spawning again when bagIn becomes true next time
        }
    }

    private void SpawnRandomItems(Vector2 position)
    {
        Item randomItem = items[GetRandomItemIndex()];
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        GameObject spawnedItem = Instantiate(randomItem.Prefab, transform.position + (Vector3)position, randomRotation, transform);
        spawnedItems.Add(spawnedItem);
    }


    private int GetRandomItemIndex()
    {
        double r = rand.NextDouble() * accumulatedWeights;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i]._weight >= r)
            {
                return i;
            }
        }
        return 0;
    }

    private void CalculateWeights()
    {
        accumulatedWeights = 0f;
        foreach (Item item in items)
        {
            accumulatedWeights += item.Chance;
            item._weight = accumulatedWeights;
        }
    }

    private void DestroySpawnedItems()
    {
        foreach (GameObject item in spawnedItems)
        {
            Destroy(item);
        }
        spawnedItems.Clear();
        swipeColorChange.childObjects.Clear();
    }

    private void OnDrawGizmos()
    {
        // Draw the spawn area box
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0));

        // Draw example spawn points if showSpawnPoints is true
        if (showSpawnPoints)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < 10; i++) // Draw 10 example spawn points
            {
                Vector2 spawnPosition = new Vector2(
                    Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                    Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f)
                );
                Gizmos.DrawSphere(transform.position + (Vector3)spawnPosition, 0.1f);
            }
        }
    }
}
