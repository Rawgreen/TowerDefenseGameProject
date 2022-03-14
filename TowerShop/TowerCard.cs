using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour
{
    public static Action<TurretSettings> OnPlaceTower;

    [SerializeField] private Image towerImage;
    [SerializeField] private TextMeshProUGUI towerCost;
    public TurretSettings TowerLoaded { get; set; }

    // TurretSettings is the scriptable object that has info about turret prefabs ...
    public void SetupTurretButton(TurretSettings towerSettings)
    {
        TowerLoaded = towerSettings;
        towerImage.sprite = towerSettings.TurretShopSprite;
        towerCost.text = towerSettings.TurretShopCost.ToString();
    }
    // when clicking a button an event going to fire, notified that player wants to place a turret in a node
    public void PlaceTower()
    {
        // buying turret mechanism
        if (CurrencySystem.Instance.TotalCoins >= TowerLoaded.TurretShopCost)
        {
            CurrencySystem.Instance.RemoveCoins(TowerLoaded.TurretShopCost);
            UIManager.Instance.CloseTowerShopPanel();
            OnPlaceTower?.Invoke(TowerLoaded);
        }
    }
}
