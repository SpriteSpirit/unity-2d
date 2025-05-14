using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private float speed = 10f;
    private float jumpForce = 10f;

    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private GameObject tail_left;
    [SerializeField] private GameObject tail_right;


    private Rigidbody2D rb;
    private PlayerHealthBar hb;
    public GameManager gm;

    private bool isGround = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hb = GetComponent<PlayerHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontal * speed * Time.deltaTime);

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].flipX = horizontal < 0;

            if (sprites[i].flipX)
            {
                tail_right.SetActive(true);
                tail_left.SetActive(false);
            }
            else
            {
                tail_right.SetActive(false);
                tail_left.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isGround = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }

        if (collision.collider.CompareTag("Bug"))
        {
            //SceneManager.LoadScene(0);
            hb.Damage();
        }

        if (collision.collider.CompareTag("Back"))
        {
            Debug.Log(collision.collider.name);
            Debug.Log(collision.collider.transform.parent.gameObject);
            isGround = true;
            Destroy(collision.collider.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            gm.AddCoin();
            Destroy(collision.gameObject);
        }
    }

}
