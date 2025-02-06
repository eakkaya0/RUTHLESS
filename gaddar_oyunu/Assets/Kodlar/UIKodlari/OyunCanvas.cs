using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO; // File için gerekli using

public class OyunCanvas : Singleton<OyunCanvas>
{
    [SerializeField] private GameObject UICanvas; 
    [SerializeField] private GameObject OyunMenu; 
    
    private bool durdumu = false; 

    

    void Start()
    {
        if (UICanvas == null)
        {
            UICanvas = GameObject.Find("UICanvas"); 
        }

        if (OyunMenu == null)
        {
            OyunMenu = GameObject.Find("OyunMenu"); 
        }

     


        
        UICanvas.SetActive(true);
        OyunMenu.SetActive(false);

    }

    void Update()
    {
       if(Input.GetButtonDown("Cancel")) {
            button_Devam();
        }
    }
    

   
    public void button_Devam()
    {
        if (!durdumu) 
        {
            Time.timeScale = 0f;
            durdumu = true;
            UICanvas.SetActive(false); 
            OyunMenu.SetActive(true); 
        }
        else 
        {
            Time.timeScale = 1f; 
            durdumu = false;
            UICanvas.SetActive(true); 
            OyunMenu.SetActive(false); 
        }
    }
    public void button_Kaydet()
    {
        GameManager.Instance.SaveGame();
    }
    public void button_Yukle()
    {
        GameManager.Instance.LoadGame();
    }
    public void button_Cikis()
    {
        Application.Quit();
    }


}
