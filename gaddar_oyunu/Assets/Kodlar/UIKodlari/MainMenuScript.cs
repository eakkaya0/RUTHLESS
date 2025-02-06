// MainMenuScript.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject GameManagerObj;
    public GameManager GameManagerSc;
    void Start()
    {
        GameManagerObj=GameObject.FindWithTag("gamemanager");
        GameManagerSc=GameManagerObj.GetComponent<GameManager>();
    }

    public void button_New_Game()
    {
        SceneManager.LoadScene(1);
    }

    public void button_Load_Game()
    {
        GameManagerSc.MainMenuLoad();
    }

    public void button_Exit_Game()
    {
        Application.Quit();
    }
}


