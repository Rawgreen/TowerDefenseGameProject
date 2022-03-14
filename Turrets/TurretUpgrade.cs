using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;

    [Header("Sell")]
    [Range(0f, 1f)]
    [SerializeField] private float sellPert;

    public float SellPerc{ get; set; }
    public int UpgradeCost { get; set; }
    public int Level { get; set; }

    public GameObject[] levels;

    int current_level = 0;

    private void Start()
    {
        UpgradeCost = upgradeInitialCost;
        SellPerc = sellPert;
        Level = 1;
    }
    /* Developer setting for upgrading turrets ( before adding upgrade button )
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Upgrade();
        }
    }
    */
    public void UpgradeTower()
    {
        if (CurrencySystem.Instance.TotalCoins >= UpgradeCost)
        {
            if (current_level < levels.Length - 1)
            {
                current_level++;
                SwitchObject(current_level);
                UpdateUpgrade();
            }
        }
    }
    public int GetSellValue()
    {
        int sellValue = Mathf.RoundToInt(UpgradeCost * SellPerc);
        return sellValue;
    }
    public void UpdateUpgrade()
    {
        CurrencySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
        Level++;
    }
    void SwitchObject(int lvl)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (i == lvl)
            {
                levels[i].SetActive(true);
            }
            else
            {
                levels[i].SetActive(false);
            }
        }
    }
}
