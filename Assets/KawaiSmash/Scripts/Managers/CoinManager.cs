using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    private int coins;
    private const string coinsKey = "coins";

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        LoadData();
        UpdateCoinText();

        MergeManager.OnMergeProcessed += MergeProcessCallback;
    }

    private void OnDestroy()
    {
        MergeManager.OnMergeProcessed -= MergeProcessCallback;
    }

    private void MergeProcessCallback(FruitType fruitType, Vector2 fruitSpawnPos)
    {
        int coinsToAdd = (int)fruitType;
        AddCoins(coinsToAdd);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        coins = Mathf.Max(0, coins);
        SaveData();
        UpdateCoinText();
    }

    public bool CanPurchase(int price)
    {
        return coins >= price;
    }

    // New method
    public void SpendCoins(int amount)
    {
        if (CanPurchase(amount))
        {
            coins -= amount;
            coins = Mathf.Max(0, coins);
            SaveData();
            UpdateCoinText();
        }
        else
        {
            Debug.LogWarning("Not enough coins to spend!");
        }
    }

    private void UpdateCoinText()
    {
        CoinText[] coinTexts = Resources.FindObjectsOfTypeAll<CoinText>();
        foreach (var coinText in coinTexts)
        {
            coinText.UpdateText(coins.ToString());
        }
    }

    private void LoadData() => coins = PlayerPrefs.GetInt(coinsKey, 0);

    private void SaveData() => PlayerPrefs.SetInt(coinsKey, coins);
}