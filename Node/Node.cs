using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // to share information which current Node wants
    public static Action<Node> OnNodeSelected;

    public static Action OnTowerSold;

    public Turret Turret { get; set; }
    public TurretUpgrade Tower { get; set; }
    public void SetTurret(Turret turret)
    {
        Turret = turret;
    }
    public void SetTower(TurretUpgrade tower)
    {
        Tower = tower;
    }
    // need to know if the node is empty so player can open the shop panel
    public bool IsEmpty()
    {
        return Tower == null;
    }
    public void SelectTower()
    {
        OnNodeSelected?.Invoke(this);
    }
    public void SellTower()
    {
        if (!IsEmpty())
        {
            // sell amount
            CurrencySystem.Instance.AddCoins(Tower.GetSellValue());
            // after selling delete gameobject
            Destroy(Tower.gameObject);
            // cut references
            Tower = null;
            // fire action
            OnTowerSold?.Invoke();
        }
    }
}
