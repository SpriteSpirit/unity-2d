using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.isClimbing = true;

        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W))
            {
                collision.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                collision.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -speed);
            }
            else
            {
                collision.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().gravityScale = 1;
        player.isClimbing = true;
    }
}
