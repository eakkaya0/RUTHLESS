using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkaPlanMuzigi : MonoBehaviour
{
    // Start is called before the first frame update
    static bool sahnedeMuzikVar;
    void Start()
    {
        if(!sahnedeMuzikVar)
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            sahnedeMuzikVar = true;
        }
        else
        {
            Destroy(gameObject);
        }
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
