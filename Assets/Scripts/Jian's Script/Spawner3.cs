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

    private void Awake()
    {
        CalculateWeights();
    }

    public int objectNum;

    private rightCheck checkOut;
    private leftCheck checkIn;
    private bool hasSpawned = false;

    void Start()
    {
        checkOut = FindObjectOfType<rightCheck>();
        checkIn = FindObjectOfType<leftCheck>();
    }

    void Update()
    {
        if (checkIn != null && checkIn.bagIn && !hasSpawned)
        {
            for (int i = 0; i < objectNum; i++)
            {
                SpawnRandomItems(new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)));
            }
            hasSpawned = true;
        }

        if (checkOut != null && checkOut.bagOut)
        {
            DestroySpawnedItems();
            hasSpawned = false; // Reset the flag to allow spawning again when bagIn becomes true next time
        }
    }

    private void SpawnRandomItems(Vector2 position)
    {
        Item randomItem = items[GetRandomItemIndex()];
        GameObject spawnedItem = Instantiate(randomItem.Prefab, position, Quaternion.identity, transform);
        spawnedItems.Add(spawnedItem);

        Debug.Log("<color=" + randomItem.Name + ">?</color> Chance: <b>" + randomItem.Chance + "</b>%");
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
    }
}


