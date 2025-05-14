using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private float speed = 5f;
    public Image bg;

    // Start is called before the first frame update
    void Start()
    {
        Color color = bg.color;
        color.a = 0f;

        // Устанавливаем обновленный цвет фона
        bg.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= 3)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (transform.position.x >= 9)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
}
