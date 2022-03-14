using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    // taking reference of TMPro
    public TextMeshProUGUI DmgText => GetComponentInChildren<TextMeshProUGUI>();
    // called at the end of damage text animation
    public void ReturnTextToPool()
    {
        // after animation has been completed, damage text won't have a parent, then return back to the pool
        transform.SetParent(null);
        ObjectPooler.ReturnToPool(gameObject);
    }
}
