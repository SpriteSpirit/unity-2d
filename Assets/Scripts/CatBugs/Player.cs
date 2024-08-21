using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    private float speed = 3f;
    private float jumpForce = 20f;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private bool isGround = true;
    private int coins = 0;
    public TMP_Text coin_text;
    public bool isClimbing = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (!isClimbing)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-speed, 0);
                sprite.flipX = horizontal < 0;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(speed, 0);
                sprite.flipX = horizontal < 0;
            }
        }
        else
        {
            rb.gravityScale = 0;

            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(0, speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(0, -speed);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isGround = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        coin_text.text = "COINS: " + coins.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
            isClimbing = false;
        }
        if (collision.collider.CompareTag("Bug"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bug"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Coin"))
        {
            coins++;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Ladder"))
        {
            transform.position = new Vector2(collision.transform.position.x + collision.gameObject.GetComponent<Renderer>().bounds.size.x, transform.position.y);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.gravityScale = 1;
            transform.position = new Vector2(collision.transform.position.x + collision.gameObject.GetComponent<Renderer>().bounds.size.x, transform.position.y);
        }
    }
}
