using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class leftCheck : MonoBehaviour
{
    public bool bagIn;
    //private SwipeColorChange swipe;

    //private rightCheck checkOut;

    // Start is called before the first frame update
    void Start()
    {
        //checkOut = FindObjectOfType<rightCheck>();
        //swipe = FindObjectOfType<SwipeColorChange>();

    }

    // Update is called once per frame
    void Update()
    {
        //if (checkOut.bagOut == true)
        //{
        //    bagIn = false;
        //}
        //else if (checkOut.bagOut == false)
        //{
        //    bagIn = true;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bag")
        {
            bagIn = true;
            //swipe.swiped = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bag")
        {
            bagIn = false;
        }
    }
}
