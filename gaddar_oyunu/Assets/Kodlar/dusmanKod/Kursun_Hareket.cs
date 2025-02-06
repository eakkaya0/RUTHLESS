using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kursun_Hareket : MonoBehaviour
{
    GaddarSaglikKontrol gaddarsaglik;

    void Start()
    {
        gaddarsaglik = FindObjectOfType<GaddarSaglikKontrol>();
        Destroy(gameObject, 10f); // Kur�un 10 saniye sonra yok olacak
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gaddarsaglik.HasarAl();
            Debug.Log("Ana karaktere hasar verildi!");
            Destroy(gameObject); // Kur�un �arp�nca yok olsun
        }
    }
}

