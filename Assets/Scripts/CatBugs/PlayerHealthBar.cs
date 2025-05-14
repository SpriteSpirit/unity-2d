using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    public int health;
    private int maxHealth = 100;
    public Slider healthBar;
    public TMP_Text healthNumberValue;
    private int damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.value = health;
        healthNumberValue.text = health + "/" + maxHealth;
    }

    public void Damage()
    {
        health -= damage;
        healthBar.value = health;
        healthNumberValue.text = health + "/" + maxHealth;

        if (health < 0)
        {
            health = 0;
            healthBar.value = health;
            healthNumberValue.text = health + "/" + maxHealth;
        }
        Debug.Log(health);
    }
}
