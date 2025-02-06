using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GaddarController : Singleton<GaddarController>
{
    Rigidbody2D rb;
    [SerializeField]
    float HareketHizi;
    [SerializeField]
    float ZiplamaGucu;
    bool yerdemi;
    bool ikikezziplayabilirmi;
    public Transform ZeminKontrolNoktasi;
    public LayerMask zeminLayer;
    public Transform AtakNoktasi;
    public LayerMask dusmanLayer;
    bool yonSagmi = true;
    Animator anim;
    public float verilenHasar;
    public float sans;
    public GameObject can;
    AudioSource asource;
  public  AudioClip saldýrý;
    

    

    // Start is called before the first frame update

    public void Start()
    {
        asource = GetComponent<AudioSource>();
    }
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HareketEttir();
        Zipla();
        YonDegistir();
        Saldir();
    }

    void HareketEttir()
    {
        float h = Input.GetAxis("Horizontal");
        float Hiz = h*HareketHizi;
        rb.velocity = new Vector2(Hiz, rb.velocity.y);
    }

    void Zipla()
    {
        yerdemi = Physics2D.OverlapCircle(ZeminKontrolNoktasi.position, .2f, zeminLayer);
        if (yerdemi)
        {
            ikikezziplayabilirmi = true;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (yerdemi)
            {
                rb.velocity = new Vector2(rb.velocity.x, ZiplamaGucu);
            }
            else
            {
                if (ikikezziplayabilirmi)
                {
                    rb.velocity = new Vector2(rb.velocity.x, ZiplamaGucu);
                    ikikezziplayabilirmi = false;
                }
            }
        }
        anim.SetFloat("hareketHizi", Math.Abs(rb.velocity.x));
        anim.SetBool("yerdemi", yerdemi);
    }

    void YonDegistir()
    {
        Vector2 geciciScale = transform.localScale;
        if (rb.velocity.x > 0)
        {
            yonSagmi = true;
            if (geciciScale.x < 0)
            {
                geciciScale.x = -geciciScale.x;
            }
        }
        else if (rb.velocity.x < 0)
        {
            yonSagmi = false;
            if (geciciScale.x > 0)
            {
                geciciScale.x = -geciciScale.x;
            }
        }
        transform.localScale = geciciScale;
    }

    void Saldir()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("saldiriyor");
        }
    }

    public void atak()
    {

        Collider2D[] dusman = Physics2D.OverlapCircleAll(AtakNoktasi.transform.position, 1f, dusmanLayer);
        foreach (Collider2D dusmanGameobject in dusman)
        {
            Debug.Log("Düþmana saldýrý yapýldý: " + dusmanGameobject.name);
            if(!asource.isPlaying)
            {
                asource.clip=saldýrý;
                asource.Play();
            }
                
            dusmanGameobject.GetComponent<DusmanSaglikSistemi>().saglik-=20;

            if (dusmanGameobject.GetComponent<DusmanSaglikSistemi>().saglik<=0)
            {
                float aralik = UnityEngine.Random.Range(0f, 100f);
                if (sans<=aralik)
                    Instantiate(can, dusmanGameobject.transform.position, dusmanGameobject.transform.rotation);

            }


        }
    }


    public void OnDrawGizmosSelected()
    {
        if (AtakNoktasi == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AtakNoktasi.position, .5f);
    }
}

