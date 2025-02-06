using UnityEngine;
using static Unity.VisualScripting.Member;

public class ToplamaYoneticisi : Singleton<ToplamaYoneticisi>
{
    LevelKod levelKod;
    UIKontrol UýKontrol;
    GaddarSaglikKontrol gaddarSaglikKontrol;
    [SerializeField]
    public bool canmi;
    public bool masummu;
    public AudioSource asource;
    public AudioClip kopek;
    public AudioClip kiz;
    public AudioClip dede;
    public AudioClip cocuk;


    private void BulVeAta()
    {
        if (levelKod == null)
            levelKod = FindObjectOfType<LevelKod>();
        if (UýKontrol == null)
            UýKontrol = FindObjectOfType<UIKontrol>();
        if (gaddarSaglikKontrol == null)
            gaddarSaglikKontrol = FindObjectOfType<GaddarSaglikKontrol>();
    }

    private void Awake()
    {
        BulVeAta();
    }

    private void Start()
    {
        asource = GetComponent<AudioSource>();
        BulVeAta(); 
    }

    private void OnEnable()
    {
        BulVeAta(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

       
        BulVeAta();

        if (masummu && levelKod != null && UýKontrol != null)
        {
            levelKod.toplanan_masum++;

            // Önce sesi çal
            if (gameObject.CompareTag("kopek"))
            {
                asource.clip = kopek;
                asource.Play();
            }
            else if (gameObject.CompareTag("kiz"))
            {
                asource.clip = kiz;
                asource.Play();
            }
            else if (gameObject.CompareTag("cocuk"))
            {
                asource.clip = cocuk;
                asource.Play();
            }
            else if (gameObject.CompareTag("dede"))
            {
                asource.clip = dede;
                asource.Play();
            }

            // Sesin tamamýnýn çalýnmasýný bekleyerek yok et
            Destroy(gameObject, asource.clip.length);
        }




        if (canmi && gaddarSaglikKontrol != null)
        {
            if (gaddarSaglikKontrol.mevcutSaglik <= gaddarSaglikKontrol.maxSaglik)
            {
                gaddarSaglikKontrol.CanAl();
                Destroy(gameObject);
            }
        }
    }
}