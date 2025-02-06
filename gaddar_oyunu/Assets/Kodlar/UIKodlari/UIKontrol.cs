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

        // UI'y� g�ncelle
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
            Debug.LogError("canText referans� atanmad�!");
            return;
        }

        if (gaddarSaglikKontrol == null)
        {
            gaddarSaglikKontrol = FindObjectOfType<GaddarSaglikKontrol>();
            if (gaddarSaglikKontrol == null)
            {
                Debug.LogError("GaddarSaglikKontrol bulunamad�!");
                return;
            }
        }

        canText.text = $"HEALTH: {gaddarSaglikKontrol.mevcutSaglik}";
        Debug.Log($"Can g�ncellendi: {gaddarSaglikKontrol.mevcutSaglik}");
    }

    public void ToplananGuncelle()
    {
        if (masumText == null) return;

        if (levelKod == null)
            levelKod = FindObjectOfType<LevelKod>();

        if (levelKod != null)
        {
            masumText.text = $"COLLECTED INNOCENT: {levelKod.toplanan_masum}";
            Debug.Log($"Toplanan masum g�ncellendi: {levelKod.toplanan_masum}");
        }
    }
}