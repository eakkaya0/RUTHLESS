using UnityEngine;

public class HasarKontrol : MonoBehaviour
{
    public GameObject hedefObjesi;
    private GaddarSaglikKontrol gaddarSaglikKontrol;

    private void Awake()
    {
        UpdateGaddarReference();
    }

    private void UpdateGaddarReference()
    {
        gaddarSaglikKontrol = FindObjectOfType<GaddarSaglikKontrol>();
        if (gaddarSaglikKontrol == null)
        {
            Debug.LogWarning("GaddarSaglikKontrol bulunamadý!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if (gaddarSaglikKontrol == null)
            {
                UpdateGaddarReference();
            }

            
            if (gaddarSaglikKontrol != null)
            {
                gaddarSaglikKontrol.HasarAl();
            }
            else
            {
                Debug.LogError("GaddarSaglikKontrol hala null! Hasar verilemedi.");
            }
        }
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

       
        if (gaddarSaglikKontrol == null)
        {
            UpdateGaddarReference();
        }
    }
}