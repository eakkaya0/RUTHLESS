using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private GameObject InGameCanvasPrefab;
   

    private KameraKontrol gameCamera;
    private GameObject PlayerGO;
    private GameObject InGameCanvas;
    private OyunCanvas ingamecanvasSC;
    public PlayerData PData;
    private bool startSceneLoading = false;
    private Scene currentScene;

    public GaddarController playerController;
    public GaddarSaglikKontrol healthController;
    public LevelKod levelController;


    void Start()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;

        currentScene = SceneManager.GetActiveScene();

        
        if (FindObjectOfType<GaddarController>() == null)
        {
            CreateInitialPlayer();
        }
        else
        {
            
            PlayerGO = FindObjectOfType<GaddarController>().gameObject;
            playerController = PlayerGO.GetComponent<GaddarController>();
            healthController = PlayerGO.GetComponent<GaddarSaglikKontrol>();

        }

        PData = new PlayerData();
        InitializePlayerData();

        if (InGameCanvasPrefab != null) 
        {
            InGameCanvas = Instantiate(InGameCanvasPrefab, Vector3.zero, Quaternion.identity);
            ingamecanvasSC = InGameCanvas.GetComponent<OyunCanvas>();

            if (ingamecanvasSC == null)
            {
                Debug.LogError("Prefab içinde 'OyunCanvas' bileþeni bulunamadý. Prefab'ý kontrol et.");
            }

            Debug.Log("Canvas baþarýyla oluþturuldu.");
            InGameCanvas.SetActive(true); 
        }
        else
        {
            Debug.LogError("InGameCanvasPrefab atanmadý! Lütfen prefab'ý kontrol et.");
        }

       

        if (currentScene.buildIndex == 0)
        {
            if (PlayerGO != null) PlayerGO.SetActive(false);
            if (InGameCanvas != null) InGameCanvas.SetActive(false);
            Debug.Log("canvas null");
            
        }

        InitializeCamera();
    }


    private void CreateInitialPlayer()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        Vector3 spawnPosition = spawnPoint != null ? spawnPoint.transform.position : Vector3.zero;

        PlayerGO = Instantiate(PlayerPrefab, spawnPosition, Quaternion.identity);
        playerController = PlayerGO.GetComponent<GaddarController>();
        healthController = PlayerGO.GetComponent<GaddarSaglikKontrol>();

        DontDestroyOnLoad(PlayerGO);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);


        levelController = FindObjectOfType<LevelKod>();

        if (PlayerGO != null)
        {
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
            if (spawnPoint != null)
            {
                PlayerGO.transform.position = spawnPoint.transform.position;
                PlayerGO.SetActive(true);
            }
        }

        if (InGameCanvas != null)
        {
            InGameCanvas.SetActive(true);
        }

        InitializeCamera();
    }

    private void InitializePlayerData()
    {
        if (healthController != null)
        {
            PData.maxHealth = healthController.maxSaglik;
            PData.currentHealth = healthController.mevcutSaglik;
        }

        if (levelController != null)
        {
            PData.collectedItems = levelController.toplanan_masum;
        }

        if (PlayerGO != null)
        {
            PData.playerPosition = PlayerGO.transform.position;
        }
    }

    public void SaveGame()
    {
        levelController = FindObjectOfType<LevelKod>();
        if (playerController != null && healthController != null && levelController != null)
        {
            PData.maxHealth = healthController.maxSaglik;
            PData.currentHealth = healthController.mevcutSaglik;
            PData.collectedItems = levelController.toplanan_masum;
            PData.playerPosition = playerController.transform.position;

            
            PData.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SaveLoad.Save(PData);
            Debug.Log("Oyun baþarýyla kaydedildi.");
        }
        else
        {
            Debug.LogWarning("Kaydetme baþarýsýz. Gerekli bileþenler eksik.");
        }
    }

    public void LoadGame()
    {
        PlayerData loadedData = SaveLoad.Load();
        if (loadedData != null)
        {
            PData = loadedData;

            if (levelController != null)
            {
                levelController.toplanan_masum = PData.collectedItems;
            }
            if (playerController != null)
            {
                playerController.transform.position = PData.playerPosition;
            }

            if (healthController != null)
            {
                healthController.maxSaglik = PData.maxHealth;
                healthController.mevcutSaglik = PData.currentHealth;
            }

           

            Debug.Log("Oyun baþarýyla yüklendi.");
        }
        else
        {
            Debug.LogWarning("Yükleme baþarýsýz. Kaydedilmiþ veri bulunamadý.");
        }
    }




    public void MainMenuStart()
    {
        startSceneLoading = true;
        SceneManager.LoadScene(1);

        // Oyuncuyu spawn noktasýna taþý
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if (spawnPoint != null && PlayerGO != null)
        {
            PlayerGO.transform.position = spawnPoint.transform.position;
        }

        // Oyuncuyu ve canvas'ý görünür yap
        if (PlayerGO != null) PlayerGO.SetActive(true);
        if (InGameCanvas != null) InGameCanvas.SetActive(true);
    }


    public void MainMenuExit()
    {
        Application.Quit();
    }


    public void MainMenuLoad()
    {
        
        PlayerData loadedData = SaveLoad.Load();
        if (loadedData != null)
        {
            PData = loadedData;

            
            SceneManager.sceneLoaded += OnSceneLoadedCallback;
            SceneManager.LoadScene(loadedData.lastSceneIndex);
        }
        else
        {
            Debug.LogWarning("Yüklenecek kayýtlý veri bulunamadý. Oyunu sýfýrdan baþlatabilirsiniz.");
        }
    }

    private void OnSceneLoadedCallback(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene loaded callback");


        
        GameObject PlayerGO = GameObject.FindWithTag("Player");
        GaddarSaglikKontrol healthController = null;
        LevelKod levelController = FindObjectOfType<LevelKod>();

        if (PlayerGO != null)
        {
            healthController = PlayerGO.GetComponent<GaddarSaglikKontrol>();

            
            if (PData != null)
            {
                PlayerGO.transform.position = PData.playerPosition;

                if (healthController != null)
                {
                    
                    healthController.maxSaglik = PData.maxHealth;
                    healthController.mevcutSaglik = PData.currentHealth;
                }

                if (levelController != null)
                {
                    
                    levelController.toplanan_masum = PData.collectedItems;
                }

                Debug.Log("Oyun baþarýyla yüklendi. Sahne: " + scene.name);
            }
        }
        else
        {
            Debug.LogError("PlayerGO bulunamadý. Sahne yükleme hatasý!");
        }

        
        SceneManager.sceneLoaded -= OnSceneLoadedCallback;
    }





    public void ResetScene()
    {
        currentScene=SceneManager.GetActiveScene();
        startSceneLoading = true;
        if (currentScene != null)
        {
            SceneManager.LoadScene(currentScene.buildIndex);

        }
        


        if (PlayerGO != null) PlayerGO.SetActive(true);
        if (InGameCanvas != null) InGameCanvas.SetActive(true);
    }
    public void LoadNextScene()
    {
        SaveGame(); 
        currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    public void ResetGame()
    {
        
        healthController = FindObjectOfType<GaddarSaglikKontrol>();
        levelController = FindObjectOfType<LevelKod>();

        
        if (healthController != null && levelController != null)
        {

            healthController.mevcutSaglik = PData.currentHealth;
            levelController.toplanan_masum =0;

        
            if (UIKontrol.Instance != null)
            {
                UIKontrol.Instance.ToplananGuncelle();
                UIKontrol.Instance.SaglikDurumunuGuncelle();

            }

          
            GameManager.Instance.ResetScene();
        }
        else
        {
            Debug.LogError("OyunuSifirla: Gerekli bileþenler bulunamadý!");
        }
    } 

   /* public void GameOver()
    {
        // Karakteri spawn noktasýna taþý
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if (spawnPoint != null && PlayerGO != null)
        {
            PlayerGO.transform.position = spawnPoint.transform.position;

            // Rigidbody2D bileþenini sýfýrla (eðer varsa)
            Rigidbody2D rb = PlayerGO.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // Hýzý sýfýrla
                rb.angularVelocity = 0f;   // Açýsal hýzý sýfýrla
            }
        }

        // Karakterin canýný 100 yap
        if (healthController != null)
        {
            healthController.mevcutSaglik = 100;
        }

        // Toplanan masum sayýsýný 0 yap
        if (levelController != null)
        {
            levelController.toplanan_masum = 0;
        }

        // UI'ý güncelle
        if (UIKontrol.Instance != null)
        {
            UIKontrol.Instance.ToplananGuncelle();
            UIKontrol.Instance.SaglikDurumunuGuncelle();
        }

        // Ana menü sahnesine geç
        SceneManager.LoadScene("ana_sahne");

        // Oyuncuyu ve canvas'ý gizle
        if (PlayerGO != null) PlayerGO.SetActive(false);
        if (InGameCanvas != null) InGameCanvas.SetActive(false);
    } */
    




    private void Update()
    {
        // Aktif sahnenin indeksi 0 ise (ana menü sahnesi)
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            InGameCanvas.SetActive(false );
        }
    }

    private void InitializeCamera()
    {
        gameCamera = FindObjectOfType<KameraKontrol>();

        if (gameCamera != null && PlayerGO != null)
        {
            gameCamera.HedefTransform = PlayerGO.transform;
            Debug.Log("kamera oyuncuyu takip ediyo");
        }
        else
        {
            Debug.LogWarning("kamera oyuncuyu takip etmiyo");
        }
    }
}