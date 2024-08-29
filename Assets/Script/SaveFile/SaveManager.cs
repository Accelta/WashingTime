using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private float autoSaveInterval = 10f; // Auto-save interval in seconds
    private float timer = 0f;

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

    private void Update()
    {
        // Increment the timer by the time passed since the last frame
        timer += Time.deltaTime;

        // Check if the timer has reached the auto-save interval
        if (timer >= autoSaveInterval)
        {
            SaveGame(); // Trigger the save
            timer = 0f; // Reset the timer
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Currency", MoneyManager.instance.currency);
        PlayerPrefs.SetInt("EmployeeLevel", EmployeeManager.instance.GetCurrentLevel());
        PlayerPrefs.SetInt("EmployeeSpeedLevel", EmployeeUpgradeUI.instance.GetCurrentSpeedLevel());

        // Save any additional data as needed
        PlayerPrefs.Save();
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
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

        // Load any additional data as needed
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
