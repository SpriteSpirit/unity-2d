using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnLadder : MonoBehaviour
{
    [SerializeField] private GameObject upStair;
    [SerializeField] private GameObject checker;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            upStair.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            upStair.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            upStair.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
