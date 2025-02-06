using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KazanmaKontrol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") &&  GameObject.FindGameObjectWithTag("kiz") == null &&GameObject.FindGameObjectWithTag("dusman")==null)
        {
            Debug.Log("Tebrikler! Tüm düþmanlarý öldürdün ve masumlarý topladýn!");
            StartCoroutine(KapanmaEkrani()); // Coroutine'i baþlatýyoruz
        }
        else
        {
            Debug.Log("Düþmanlarý öldür ve masumlarý topla!");
        }
    }

    IEnumerator KapanmaEkrani()
    {
        yield return new WaitForSeconds(5f); // 5 saniye bekler
        Debug.Log("Oyun kapatýlýyor...");
       Application.Quit(); // Gerçek oyunda oyun kapanýr

    }
}
