using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] private TMP_Text coinText;

    void Start()
    {
        coinText.text = "COINS: " + coins.ToString();
    }

    public void AddCoin()
    {
        coins++;
        coinText.text = "COINS: " + coins.ToString();
        Debug.Log("coins");
        Debug.Log(coins);
    }

}
