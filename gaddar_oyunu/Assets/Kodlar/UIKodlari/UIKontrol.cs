using UnityEngine;
using TMPro;

public class UIKontrol : Singleton<UIKontrol>
{
    [SerializeField] private TMP_Text canText;
    [SerializeField] private TMP_Text masumText;

    private GaddarSaglikKontrol gaddarSaglikKontrol;
    private LevelKod levelKod;

    void OnEnable()
    {
        
        BulVeAta();
    }

    private void BulVeAta()
    {
        if (gaddarSaglikKontrol == null)
            gaddarSaglikKontrol = FindObjectOfType<GaddarSaglikKontrol>();

        if (levelKod == null)
            levelKod = FindObjectOfType<LevelKod>();

        // UI'yý güncelle
        SaglikDurumunuGuncelle();
        ToplananGuncelle();
    }
    private void Update()
    {
        SaglikDurumunuGuncelle();
        ToplananGuncelle();
    }

    public void SaglikDurumunuGuncelle()
    {
        if (canText == null)
        {
            Debug.LogError("canText referansý atanmadý!");
            return;
        }

        if (gaddarSaglikKontrol == null)
        {
            gaddarSaglikKontrol = FindObjectOfType<GaddarSaglikKontrol>();
            if (gaddarSaglikKontrol == null)
            {
                Debug.LogError("GaddarSaglikKontrol bulunamadý!");
                return;
            }
        }

        canText.text = $"HEALTH: {gaddarSaglikKontrol.mevcutSaglik}";
        Debug.Log($"Can güncellendi: {gaddarSaglikKontrol.mevcutSaglik}");
    }

    public void ToplananGuncelle()
    {
        if (masumText == null) return;

        if (levelKod == null)
            levelKod = FindObjectOfType<LevelKod>();

        if (levelKod != null)
        {
            masumText.text = $"COLLECTED INNOCENT: {levelKod.toplanan_masum}";
            Debug.Log($"Toplanan masum güncellendi: {levelKod.toplanan_masum}");
        }
    }
}