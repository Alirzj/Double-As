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

    private void Awake()
    {
        CalculatedWeights();
    }

    public int objectNum;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < objectNum; i++)
        {
            SpawnRandomItems(new Vector2(Random.Range(-3f, 3f), Random.Range(-4f, 4f)));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnRandomItems(Vector2 position)
    {
        Item randomItem = items[GetRandomItemIndex()];

        Instantiate(randomItem.Prefab, position, Quaternion.identity, transform);


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

    private void CalculatedWeights()
    {
        accumulatedWeights = 0f;
        foreach (Item item in items)
        {
            accumulatedWeights += item.Chance;
            item._weight = accumulatedWeights;
        }
    }
}

