using UnityEngine;

public class KameraKontrol : MonoBehaviour
{
    [SerializeField] public Transform HedefTransform;
    [SerializeField] float minY, MaxY;
    [SerializeField] Transform Zemin;
    Vector2 sonPos;

    void Start()
    {
        sonPos = transform.position;
    }

    void Update()
    {
        if (HedefTransform != null)
        {
            KamerayiSinirla();
            ZeminiHareketEttir();
            sonPos = transform.position;
        }
    }

    void KamerayiSinirla()
    {
        transform.position = new Vector3(
            HedefTransform.position.x,
            Mathf.Clamp(HedefTransform.position.y, minY, MaxY),
            transform.position.z
        );
    }

    void ZeminiHareketEttir()
    {
        Vector2 hareketMiktari = (Vector2)transform.position - sonPos;
        Zemin.position += new Vector3(hareketMiktari.x, hareketMiktari.y, 0f);
    }
}