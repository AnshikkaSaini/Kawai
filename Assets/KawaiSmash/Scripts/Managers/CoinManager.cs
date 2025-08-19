using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    private int coins;
    private const string coinsKey = "coins";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadData();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        coins = Mathf.Max(0, coins);
        SaveData();
        UpdateCoinText();
    }

  

    public bool CanPurchase( int price)
    {
        return coins >= price;
    }

    private void UpdateCoinText()
    {
        
    }

    private void LoadData() => coins = PlayerPrefs.GetInt(coinsKey);

    private void SaveData() => PlayerPrefs.SetInt(coinsKey, coins);
}
    
