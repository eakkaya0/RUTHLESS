using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DusmanSaglikSistemi : MonoBehaviour
{

    public int saglik;
    Animator anim;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (saglik <= 0) {
            anim.SetTrigger("hasaraldimi");

            Destroy(gameObject,.50f);
        
        }
    }
}
