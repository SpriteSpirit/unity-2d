using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 0.1f;
    public Vector3[] positions;
    private int indexPosition;
    private SpriteRenderer sprite;
    [SerializeField] private bool isFlip;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, positions[indexPosition], speed);

        if (transform.position == positions[indexPosition])
        {
            if (indexPosition < positions.Length - 1)
            {
                indexPosition++;
                if (isFlip)
                { sprite.flipX = false; }
                else
                { sprite.flipX = true; }
            }
            else
            {
                indexPosition = 0;

                if (isFlip)
                { sprite.flipX = true; }
                else 
                { sprite.flipX = false; }
            }
        }
    }
}
