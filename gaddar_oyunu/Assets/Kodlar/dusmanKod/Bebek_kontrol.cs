using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bebek_kontrol : MonoBehaviour
{
    public AudioSource musicSource; // M�zik �alacak AudioSource
    public GameObject mermiPrefab; // Mermi prefab�
    public Transform bebekTransform; // Bebe�in transformu
    public float minMusicDuration = 2f; // M�zi�in minimum �alma s�resi
    public float maxMusicDuration = 5f; // M�zi�in maksimum �alma s�resi
    public float minPauseDuration = 1f; // M�zi�in minimum durma s�resi
    public float maxPauseDuration = 3f; // M�zi�in maksimum durma s�resi
    public float mermiHizi = 70f; // Merminin h�z�

    private bool isMusicPlaying = false;
    private bool isPlayerInRange = false;
    private float nextActionTime = 0f;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        // Ba�lang��ta m�zi�i durdur
        musicSource.Stop();
    }

    void Update()
    {
        // Ana karakterin x pozisyonunu kontrol et
        float playerX = GameObject.FindWithTag("Player"). transform.position.x;

        if (playerX >= 270 && playerX <= 380)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
            musicSource.Stop();
            isMusicPlaying = false;
        }

        if (isPlayerInRange)
        {
            if (Time.time >= nextActionTime)
            {
                if (isMusicPlaying)
                {
                    // M�zik �al�yorsa durdur ve rastgele bir s�re beklet
                    musicSource.Stop();
                    isMusicPlaying = false;
                    nextActionTime = Time.time + Random.Range(minPauseDuration, maxPauseDuration);
                }
                else
                {
                    // M�zik �alm�yorsa ba�lat ve rastgele bir s�re �al
                    musicSource.Play();
                    isMusicPlaying = true;
                    nextActionTime = Time.time + Random.Range(minMusicDuration, maxMusicDuration);
                }
            }

            // M�zik �almazken ana karakter hareket ediyorsa mermi olu�tur
            if (!isMusicPlaying && Input.GetAxis("Horizontal") != 0)
            {
                // Mermiyi olu�tur
                MermiAt();
                
            }
        }

       
    }

    void MermiAt()
    {
        GameObject mermi = Instantiate(mermiPrefab, bebekTransform.position, Quaternion.identity);

        // Merminin Rigidbody2D bile�enini al
        Rigidbody2D mermiRigidbody = mermi.GetComponent<Rigidbody2D>();

        // Oyuncunun pozisyonunu al
        Vector2 oyuncuPozisyonu = GameObject.FindWithTag ("Player").transform.position;

        // Merminin y�n�n� hesapla (oyuncuya do�ru)
        Vector2 yon = (oyuncuPozisyonu - (Vector2)bebekTransform.position).normalized;

        // Mermiye h�z ver
        mermiRigidbody.velocity = yon * mermiHizi;

        
    }

    
}