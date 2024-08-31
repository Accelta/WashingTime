using System.Collections;
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
        LoadGame(); // Load the game when the scene starts
        EnsureEmployeeUpgradeUI();
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
    InitializeWashingMachines(); // Ensure washing machines are initialized before saving

    PlayerPrefs.SetInt("Currency", MoneyManager.instance.currency);
    PlayerPrefs.SetInt("EmployeeLevel", EmployeeManager.instance.GetCurrentLevel());

    // Only save EmployeeSpeedLevel if the UI is active
    if (EmployeeUpgradeUI.instance != null && EmployeeUpgradeUI.instance.gameObject.activeInHierarchy)
    {
        PlayerPrefs.SetInt("EmployeeSpeedLevel", EmployeeUpgradeUI.instance.GetCurrentSpeedLevel());
    }
    else
    {
        Debug.LogWarning("EmployeeUpgradeUI instance is null or inactive. Skipping save for employee speed level.");
    }

    // Save washing machine levels
    if (washingMachines != null)
    {
        for (int i = 0; i < washingMachines.Length; i++)
        {
            PlayerPrefs.SetInt("WashingMachineLevel_" + i, washingMachines[i].UpgradeLevel);
        }
    }

    PlayerPrefs.Save(); // Ensure PlayerPrefs is saved immediately
    Debug.Log("Game Saved!");
}

    public void LoadGame()
{
    InitializeWashingMachines(); // Ensure washing machines are initialized before loading

    if (PlayerPrefs.HasKey("Currency"))
    {
        MoneyManager.instance.currency = PlayerPrefs.GetInt("Currency");
    }

    if (PlayerPrefs.HasKey("EmployeeLevel"))
    {
        int savedLevel = PlayerPrefs.GetInt("EmployeeLevel");
        EmployeeManager.instance.SetCurrentLevel(savedLevel);
    }

    // Store the speed level in a temporary variable if the UI is inactive
    if (PlayerPrefs.HasKey("EmployeeSpeedLevel"))
    {
        int savedSpeedLevel = PlayerPrefs.GetInt("EmployeeSpeedLevel");

        if (EmployeeUpgradeUI.instance != null && EmployeeUpgradeUI.instance.gameObject.activeInHierarchy)
        {
            EmployeeUpgradeUI.instance.SetCurrentSpeedLevel(savedSpeedLevel);
        }
        else
        {
            StartCoroutine(WaitForEmployeeUIToLoad(savedSpeedLevel));
        }
    }

    // Load washing machine levels
    if (washingMachines != null)
    {
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
    }

    Debug.Log("Game Loaded!");
}

private IEnumerator WaitForEmployeeUIToLoad(int savedSpeedLevel)
{
    while (EmployeeUpgradeUI.instance == null || !EmployeeUpgradeUI.instance.gameObject.activeInHierarchy)
    {
        yield return null; // Wait until EmployeeUpgradeUI is active
    }

    EmployeeUpgradeUI.instance.SetCurrentSpeedLevel(savedSpeedLevel);
    Debug.Log("Employee Speed Level Loaded After UI Activation");
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
public void resetSave()
{
    PlayerPrefs.DeleteAll();
    Debug.Log("PlayerPrefs deleted.");
    
    // Initialize the game state as if it's the first time
    if (MoneyManager.instance != null)
    {
        MoneyManager.instance.currency = 1000000; // or any default value
    }

    if (EmployeeManager.instance != null)
    {
        EmployeeManager.instance.SetCurrentLevel(0);
    }

    if (EmployeeUpgradeUI.instance != null)
    {
        EmployeeUpgradeUI.instance.SetCurrentSpeedLevel(0);
    }

    InitializeWashingMachines();
    if (washingMachines != null)
    {
        foreach (var machine in washingMachines)
        {
            machine.Unlock(); // Unlock and reset machine state as needed
        }
    }

    SaveGame(); // Save the reset state
    Debug.Log("Game reset and saved.");
}
private void EnsureEmployeeUpgradeUI()
{
    if (EmployeeUpgradeUI.instance == null)
    {
        Debug.LogError("EmployeeUpgradeUI instance is null. Cannot proceed with saving or loading.");
    }
}
}
