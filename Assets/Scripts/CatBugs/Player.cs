using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float speed = 3f;
    private float jumpForce = 15f;
    private int coins = 0;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    public TMP_Text coin_text;

    public bool isClimbing = false;
    private bool isGround = true;


    public Image bg;
    public float bgSpeedMultiplier = 0.1f; // ��������� �������� ����

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
                rb.linearVelocity = new Vector2(-speed, 0);
                sprite.flipX = horizontal < 0;
                MoveBackground(-speed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rb.linearVelocity = new Vector2(speed, 0);
                sprite.flipX = horizontal < 0;
                MoveBackground(speed);
            }
        }
        else
        {
            rb.gravityScale = 0;

            if (Input.GetKey(KeyCode.W))
            {
                rb.linearVelocity = new Vector2(0, speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rb.linearVelocity = new Vector2(0, -speed);
            }
            else
            {
                rb.linearVelocity = new Vector2(0, 0);
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

    void MoveBackground(float playerSpeed)
    {
        Vector2 currentPos = bg.rectTransform.anchoredPosition;
        // �������� ���� � ��������������� �����������
        currentPos.x += playerSpeed * bgSpeedMultiplier * Time.deltaTime;
        bg.rectTransform.anchoredPosition = currentPos;
    }
}
