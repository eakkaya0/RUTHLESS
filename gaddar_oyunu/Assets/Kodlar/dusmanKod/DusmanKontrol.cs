using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanKontrol :MonoBehaviour
{
    public GameObject hedefObjesi; 
    public Transform solSinir; 
    public Transform sagSinir; 
    public float takipHizi = 2f; 
    private Rigidbody2D rb; 
    private Animator animator;

    public Transform DusmanAtakNoktasi;
    public LayerMask GaddarLayer;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        hedefObjesi = GameObject.FindWithTag("Player");
        if (hedefObjesi == null)
        {
            Debug.LogError("Ana karakter atanmadý! Lütfen hedefObjesi deðiþkenine karakterinizi atayýn.");
        }
        solSinir.parent=null;
        sagSinir.parent=null;
    }

    public void atak() 
    {
        Collider2D[] gaddar = Physics2D.OverlapCircleAll(DusmanAtakNoktasi.transform.position, .5f, GaddarLayer);
        foreach (Collider2D gaddarGameobject in gaddar)
        {
            Debug.Log("Gaddara saldýrý yapýldý: " + gaddarGameobject.name);
            gaddarGameobject.GetComponent<GaddarSaglikKontrol>().HasarAl();
        }
    }
    public void OnDrawGizmosSelected()
    {
        if (DusmanAtakNoktasi == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(DusmanAtakNoktasi.position, .5f);
    }


    private void Update()
    {
        if (hedefObjesi == null)
        {
            hedefObjesi = GameObject.FindWithTag("Player");
            if (hedefObjesi == null)
            {
                Debug.LogWarning("Player tag'li bir nesne bulunamadý!");
            }
        }

        Transform hedefTransform = hedefObjesi.transform;
        float mesafe = Vector2.Distance(transform.position, hedefTransform.position);

        if (mesafe <= 0.5f) 
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("yuruyor", false);
            animator.SetTrigger("saldiriyor"); 
        }
        else if (mesafe > 0.5f && mesafe <= 24f && 
            hedefTransform.position.x >= solSinir.position.x &&
            hedefTransform.position.x <= sagSinir.position.x)
        {
            Vector2 takipYonu = (hedefTransform.position - transform.position).normalized;
            rb.velocity = new Vector2(takipYonu.x * takipHizi, rb.velocity.y);
            animator.SetBool("yuruyor", true); 

            
            if (takipYonu.x > 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (takipYonu.x < 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else 
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("yuruyor", false);
        }
    }

    
}
