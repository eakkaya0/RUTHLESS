using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bitisNoktasi : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") &&
    GameObject.FindGameObjectWithTag("kiz") == null &&GameObject.FindGameObjectWithTag("dede")==null&&
    GameObject.FindGameObjectWithTag("cocuk")==null&&GameObject.FindGameObjectWithTag("kopek") == null&&
    GameObject.FindGameObjectWithTag("dusman") == null)
        {
            GameManager.Instance.LoadNextScene();
        }
        else
        {
            Debug.Log("dusmanlarý oldur masumlarý topla");
        }
    }
}
