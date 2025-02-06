using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KazanmaKontrol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") &&  GameObject.FindGameObjectWithTag("kiz") == null &&GameObject.FindGameObjectWithTag("dusman")==null)
        {
            Debug.Log("Tebrikler! T�m d��manlar� �ld�rd�n ve masumlar� toplad�n!");
            StartCoroutine(KapanmaEkrani()); // Coroutine'i ba�lat�yoruz
        }
        else
        {
            Debug.Log("D��manlar� �ld�r ve masumlar� topla!");
        }
    }

    IEnumerator KapanmaEkrani()
    {
        yield return new WaitForSeconds(5f); // 5 saniye bekler
        Debug.Log("Oyun kapat�l�yor...");
       Application.Quit(); // Ger�ek oyunda oyun kapan�r

    }
}
