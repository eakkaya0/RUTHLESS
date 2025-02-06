using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class GaddarSaglikKontrol:Singleton<GaddarSaglikKontrol>
{
    // Start is called before the first frame update



    public int maxSaglik;
    public int mevcutSaglik;

    Animator anim;

    public UIKontrol uikontrol;
    GaddarController gaddarcontroller;
    LevelKod levelyonetici;

    AudioSource asource2;
   public AudioClip yaralanma;
    

    void OnEnable()
    {
        asource2= GetComponent<AudioSource>();
        Debug.Log($"{gameObject.name} is now active!");
        uikontrol = Object.FindObjectOfType<UIKontrol>();
        if (uikontrol == null)
        {
            Debug.LogError("UIKontrol bulunamadý!");
        }

        anim = GetComponent<Animator>();
        gaddarcontroller = Object.FindObjectOfType<GaddarController>();
        levelyonetici = Object.FindAnyObjectByType<LevelKod>();

        mevcutSaglik = maxSaglik;
    }

    // Update is called once per frame
    void Update()
    {
        if (gaddarcontroller != null && gaddarcontroller.transform.position.y <= -30f)
        {
            Debug.Log("Düþme tespit edildi - Update içinde");
            GameManager.Instance.ResetGame();
        }
    }


    public void HasarAl()
    {
        anim.SetTrigger("hasaraldimi");
        if (!asource2. isPlaying)
        {

            asource2.clip=yaralanma;
            asource2.Play();
        }
        mevcutSaglik -= 10;
        Debug.Log($"Saðlýk: {mevcutSaglik}");
        //uikontrol.SaglikDurumunuGuncelle();

        if (mevcutSaglik <= 0)
        {
            GameManager.Instance.ResetGame();
        }
    }

    

    public void CanAl()
    {
        mevcutSaglik+=10;
        if (mevcutSaglik>=maxSaglik)
        {
            mevcutSaglik=maxSaglik;
        }
        //uikontrol.SaglikDurumunuGuncelle();
    }
}
