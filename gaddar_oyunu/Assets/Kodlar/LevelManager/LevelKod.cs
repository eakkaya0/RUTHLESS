using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelKod : Singleton<LevelKod>
{

    public int toplanan_masum;
    // Start is called before the first frame update

    private void Awake()
    {
        base.Awake();
        Debug.Log("level kod awake");
       
    }
    void Start()
    {
        Debug.Log("level kod start");
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
