using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagChecker : MonoBehaviour
{
    public bool hasBadObject = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bag"))
        {
            CheckForBadTag(collision.gameObject);
        }
    }

    private void CheckForBadTag(GameObject bag)
    {
        hasBadObject = false; // Reset to false before checking

        foreach (Transform child in bag.transform)
        {
            if (child.CompareTag("Bad"))
            {
                hasBadObject = true;
                Debug.Log("bag has bad object");
                break; // Exit loop early if a bad object is found
            }
        }
    }
}