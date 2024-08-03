using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endCollider : MonoBehaviour
{
    private BagDestroy1 conveyorBelt;

    // Start is called before the first frame update
    void Start()
    {
        conveyorBelt = FindObjectOfType<BagDestroy1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == conveyorBelt.bag1 || collision.gameObject == conveyorBelt.bag2)
        {
            conveyorBelt.RespawnBag(collision.gameObject);
        }
    }
}
