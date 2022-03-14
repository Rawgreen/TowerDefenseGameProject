using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
                               // using generic singleton
public class DamageTextManager : Singleton<DamageTextManager>
{
    public ObjectPooler Pooler { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Pooler = GetComponent<ObjectPooler>(); 
    }
}
