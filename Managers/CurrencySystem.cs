using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : Singleton<CurrencySystem>
{
    [SerializeField] private int coinTest;

    // save total coins in the game
    private string CURRENCY_SAVE_KEY = "MYGAME_CURRENCY";

    public int TotalCoins { get; set; }
    private void Start()
    {
        PlayerPrefs.DeleteKey(CURRENCY_SAVE_KEY);
        LoadCoins();
    }
    // saving into a file, so player can keep currency amount
    private void LoadCoins()
    {
        TotalCoins = PlayerPrefs.GetInt(CURRENCY_SAVE_KEY, coinTest);
    }
    public void AddCoins(int amount)
    {
        TotalCoins += amount;
        // set currency amount into variable
        PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, amount);
        // saving into file
        PlayerPrefs.Save();
    }
    public void RemoveCoins(int amount)
    {
        if (TotalCoins >= amount)
        {
            TotalCoins -= amount;
            PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, amount);
            PlayerPrefs.Save();
        }
    }
    // amount of added coin when an enemy killed
    private void AddCoins(Enemy enemy)
    {
        AddCoins(100);
    }
    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += AddCoins;
    }
    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= AddCoins;
    }
}
