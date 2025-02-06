using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KopekKontrol : MonoBehaviour
{
    [SerializeField] Transform solNokta; 
    [SerializeField] Transform sagNokta; 
    [SerializeField] float hareketHizi = 2f; 

    private bool sagaGidiyor = false; 

    void Start()
    {
        solNokta.parent = null;
        sagNokta.parent = null;

        YonDegistir(-1); 
    }

    void Update()
    {
        HareketEt();
    }

    void HareketEt()
    {
        if (sagaGidiyor)
        {
            transform.position = Vector2.MoveTowards(transform.position, sagNokta.position, hareketHizi * Time.deltaTime);

            
            if (Vector2.Distance(transform.position, sagNokta.position) < 0.1f)
            {
                sagaGidiyor = false;
                YonDegistir(-1); 
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, solNokta.position, hareketHizi * Time.deltaTime);

            
            if (Vector2.Distance(transform.position, solNokta.position) < 0.1f)
            {
                sagaGidiyor = true;
                YonDegistir(1); 
            }
        }
    }

    void YonDegistir(float yon)
    {
       
        Vector3 yeniRotation = transform.eulerAngles;
        yeniRotation.y = (yon == 1) ? 0 : 180; 
        transform.eulerAngles = yeniRotation;
    }
}
