
// using System;
// using Script;
// using Script.SO;
// using UnityEngine;

// public class GameManager : MonoBehaviour
// {
//     public DialogueManager DialogueManager;
//     public QuestManager questManager;
//     public ShowWashUI showWashUI;
//     public InteractAbility PlayerInteract;
//     public Inventory   Inventory;
//     [SerializeField] private int itemCollect = 0;
//     [SerializeField] private AudioClip suaraambil;
//     [SerializeField] private AudioClip suaraambil2;

//     private static GameManager _instance;
//     [SerializeField] private int maxItemToUnlock = 5;

//     public static GameManager Instance
//     {
//         get
//         {
//             if (_instance == null)
//             {
//                 // Find the GameManager in the scene
//                 _instance = FindObjectOfType<GameManager>();

//                 if (_instance == null)
//                 {
//                     // If there is no GameManager in the scene, create one
//                     GameObject singleton = new GameObject("GameManager");
//                     _instance = singleton.AddComponent<GameManager>();
//                     DontDestroyOnLoad(singleton);

//                     // Initialize components on first creation
//                     _instance.InitializeComponents();
//                 }
//             }

//             return _instance;
//         }
//     }

//     // Prevent instantiation
//     private GameManager() { }

//     private void Awake()
//     {
//         // Ensure singleton instance
//         if (_instance == null)
//         {
//             _instance = this;
//             DontDestroyOnLoad(gameObject);
//             InitializeComponents();
//         }
//         else if (_instance != this)
//         {
//             Destroy(gameObject);
//         }
//     }

//     // Method to initialize or reinitialize components
//     public void InitializeComponents()
//     {
//         DialogueManager = FindObjectOfType<DialogueManager>();
//         questManager = FindObjectOfType<QuestManager>();
//         showWashUI = FindObjectOfType<ShowWashUI>();
//         PlayerInteract = FindObjectOfType<InteractAbility>();
//         Inventory = FindObjectOfType<Inventory>();
//         // Reinitialize AudioClip references if needed
//         if (suaraambil == null)
//         {
//             // Assuming these clips are stored as resources, replace with appropriate path or method to load clips
//             suaraambil = Resources.Load<AudioClip>("Assets/Audio & Video/yee dapat satu baju.wav");
//         }
//         if (suaraambil2 == null)
//         {
//             // Assuming these clips are stored as resources, replace with appropriate path or method to load clips
//             suaraambil2 = Resources.Load<AudioClip>("Assets/Audio & Video/kurang.wav");
//         }
//     }

//     // Method to reset game state
//     public void ResetGameState()
//     {
//         itemCollect = 0;
//         // Reset other game state variables as needed
//     }

//     public void AddBaju()
//     {
//         itemCollect++;

//         switch (itemCollect)
//         {
//             case 1:
//                 AudioSource.PlayClipAtPoint(suaraambil, Camera.main.transform.position);
//                 break;
//             case 4:
//                 AudioSource.PlayClipAtPoint(suaraambil2, Camera.main.transform.position);
//                 break;
//             default:
//                 break;
//         }

//         PlayerInteract.currectInteractObj = null;
//         if (itemCollect == maxItemToUnlock)
//         {
//             questManager.ChangeQuest(3);
//             showWashUI._canInteract = true;
//         }
//     }

//     public bool TryPickup(BajuSo bajuSo)
//     {
//         if(Inventory.CurrectBajuSo ==  null){
//             Inventory.SetCurretBaju(bajuSo);
//             return true;
//         }else{
//             return false;
//         }
//     }

//     // Add your GameManager methods and properties here
// }using System;
using Script;
using Script.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public DialogueManager DialogueManager;
    public QuestManager questManager;
    public ShowWashUI showWashUI;
    public InteractAbility PlayerInteract;
    public Inventory Inventory;
    [SerializeField] private int itemCollect = 0;

    private static GameManager _instance;
    [SerializeField] private int maxItemToUnlock = 5;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Create a new GameManager if one doesn't already exist
                GameObject singleton = new GameObject("GameManager");
                _instance = singleton.AddComponent<GameManager>();
                DontDestroyOnLoad(singleton);

                // Initialize components on first creation
                _instance.InitializeComponents();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // Ensure singleton instance
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeComponents();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Register to the sceneLoaded event
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister from the sceneLoaded event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeComponents();
    }

    // Method to initialize or reinitialize components
    public void InitializeComponents()
    {
        DialogueManager = FindObjectOfType<DialogueManager>();
        questManager = FindObjectOfType<QuestManager>();
        showWashUI = FindObjectOfType<ShowWashUI>();
        PlayerInteract = FindObjectOfType<InteractAbility>();
        Inventory = FindObjectOfType<Inventory>(); // Try to find Inventory component
    }

    // Method to reset game state
    public void ResetGameState()
    {
        itemCollect = 0;
        // Reset other game state variables as needed
    }

    public void AddBaju()
    {
        itemCollect++;
        var bajuItem = FindObjectOfType<BajuItem>();
        if (bajuItem != null)
        {
            switch (itemCollect)
            {
                case 1:
                    if (bajuItem.suaraAmbilClip != null)
                    {
                        AudioSource.PlayClipAtPoint(bajuItem.suaraAmbilClip, Camera.main.transform.position);
                    }
                    break;
                case 4:
                    if (bajuItem.suaraAmbil2Clip != null)
                    {
                        AudioSource.PlayClipAtPoint(bajuItem.suaraAmbil2Clip, Camera.main.transform.position);
                    }
                    break;
                default:
                    break;
            }
        }

        PlayerInteract.currectInteractObj = null;
        if (itemCollect == maxItemToUnlock)
        {
            questManager.ChangeQuest(3);
            showWashUI._canInteract = true;
        }
    }

    public bool TryPickup(BajuSo bajuSo)
    {
        if (Inventory != null && Inventory.CurrectBajuSo == null)
        {
            Inventory.SetCurretBaju(bajuSo);
            return true;
        }
        else
        {
            return false;
        }
    }
}




