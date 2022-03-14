using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// to create scriptable object
[CreateAssetMenu(fileName = " Turret Shop Setting ")]

// inheriting scriptable object
public class TurretSettings : ScriptableObject
{
     public GameObject TurretPrefab;
     public int TurretShopCost;
     public Sprite TurretShopSprite;
}
