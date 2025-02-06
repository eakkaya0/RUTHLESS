using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bebek_kontrol : MonoBehaviour
{
    public AudioSource musicSource; // Müzik çalacak AudioSource
    public GameObject mermiPrefab; // Mermi prefabý
    public Transform bebekTransform; // Bebeðin transformu
    public float minMusicDuration = 2f; // Müziðin minimum çalma süresi
    public float maxMusicDuration = 5f; // Müziðin maksimum çalma süresi
    public float minPauseDuration = 1f; // Müziðin minimum durma süresi
    public float maxPauseDuration = 3f; // Müziðin maksimum durma süresi
    public float mermiHizi = 70f; // Merminin hýzý

    private bool isMusicPlaying = false;
    private bool isPlayerInRange = false;
    private float nextActionTime = 0f;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        // Baþlangýçta müziði durdur
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
                    // Müzik çalýyorsa durdur ve rastgele bir süre beklet
                    musicSource.Stop();
                    isMusicPlaying = false;
                    nextActionTime = Time.time + Random.Range(minPauseDuration, maxPauseDuration);
                }
                else
                {
                    // Müzik çalmýyorsa baþlat ve rastgele bir süre çal
                    musicSource.Play();
                    isMusicPlaying = true;
                    nextActionTime = Time.time + Random.Range(minMusicDuration, maxMusicDuration);
                }
            }

            // Müzik çalmazken ana karakter hareket ediyorsa mermi oluþtur
            if (!isMusicPlaying && Input.GetAxis("Horizontal") != 0)
            {
                // Mermiyi oluþtur
                MermiAt();
                
            }
        }

       
    }

    void MermiAt()
    {
        GameObject mermi = Instantiate(mermiPrefab, bebekTransform.position, Quaternion.identity);

        // Merminin Rigidbody2D bileþenini al
        Rigidbody2D mermiRigidbody = mermi.GetComponent<Rigidbody2D>();

        // Oyuncunun pozisyonunu al
        Vector2 oyuncuPozisyonu = GameObject.FindWithTag ("Player").transform.position;

        // Merminin yönünü hesapla (oyuncuya doðru)
        Vector2 yon = (oyuncuPozisyonu - (Vector2)bebekTransform.position).normalized;

        // Mermiye hýz ver
        mermiRigidbody.velocity = yon * mermiHizi;

        
    }

    
}