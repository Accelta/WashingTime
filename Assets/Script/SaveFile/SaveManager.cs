using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private WashingMachine[] washingMachines;
    private float autoSaveInterval = 15f; // Auto-save interval in seconds
    private float autoSaveTimer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeWashingMachines();
        autoSaveTimer = autoSaveInterval; // Initialize the auto-save timer
    }

    private void Update()
    {
        // Count down the auto-save timer
        autoSaveTimer -= Time.deltaTime;

        // Check if it's time to auto-save
        if (autoSaveTimer <= 0f)
        {
            SaveGame(); // Perform the auto-save
            autoSaveTimer = autoSaveInterval; // Reset the timer
        }
    }

    private void InitializeWashingMachines()
    {
        washingMachines = FindObjectsOfType<WashingMachine>();
    }

    public void SaveGame()
    {
        InitializeWashingMachines(); // Initialize washing machines before saving

        PlayerPrefs.SetInt("Currency", MoneyManager.instance.currency);
        PlayerPrefs.SetInt("EmployeeLevel", EmployeeManager.instance.GetCurrentLevel());
        PlayerPrefs.SetInt("EmployeeSpeedLevel", EmployeeUpgradeUI.instance.GetCurrentSpeedLevel());

        // Save washing machine levels
        for (int i = 0; i < washingMachines.Length; i++)
        {
            PlayerPrefs.SetInt("WashingMachineLevel_" + i, washingMachines[i].UpgradeLevel);
        }

        PlayerPrefs.Save();
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        InitializeWashingMachines(); // Initialize washing machines before loading

        if (PlayerPrefs.HasKey("Currency"))
        {
            MoneyManager.instance.currency = PlayerPrefs.GetInt("Currency");
        }

        if (PlayerPrefs.HasKey("EmployeeLevel"))
        {
            int savedLevel = PlayerPrefs.GetInt("EmployeeLevel");
            EmployeeManager.instance.SetCurrentLevel(savedLevel);
        }

        if (PlayerPrefs.HasKey("EmployeeSpeedLevel"))
        {
            int savedSpeedLevel = PlayerPrefs.GetInt("EmployeeSpeedLevel");
            EmployeeUpgradeUI.instance.SetCurrentSpeedLevel(savedSpeedLevel);
        }

        // Load washing machine levels
        for (int i = 0; i < washingMachines.Length; i++)
        {
            if (PlayerPrefs.HasKey("WashingMachineLevel_" + i))
            {
                int savedLevel = PlayerPrefs.GetInt("WashingMachineLevel_" + i);
                for (int j = 0; j < savedLevel; j++)
                {
                    washingMachines[i].Upgrade(); // Upgrade the machine to the saved level
                }
            }
        }

        Debug.Log("Game Loaded!");
    }

    private void OnApplicationQuit()
    {
        SaveGame(); // Auto-save when the application is about to quit
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame(); // Auto-save when the application is paused (backgrounded on mobile)
        }
    }
}
