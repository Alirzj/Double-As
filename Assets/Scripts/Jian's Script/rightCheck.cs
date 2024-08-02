using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightCheck : MonoBehaviour
{
    public bool bagOut;
    //private leftCheck checkIn;

    // Start is called before the first frame update
    void Start()
    {
        //checkIn = FindObjectOfType<leftCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (checkIn.bagIn == true)
        //{
        //    bagOut = false;
        //}
        //else if (checkIn.bagIn == false)
        //{
        //    bagOut = true;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bag")
        {
            bagOut = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bag")
        {
            bagOut = false;
        }
    }
}
