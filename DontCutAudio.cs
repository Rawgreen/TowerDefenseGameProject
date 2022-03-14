using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontCutAudio : MonoBehaviour
{
    public static DontCutAudio DCInstance;

    void Awake()
    {
        if(DCInstance!=null && DCInstance!=this)
        {
            Destroy(this.gameObject);
            return;
        }

        DCInstance = this;
        DontDestroyOnLoad(this);
    }

    

}
