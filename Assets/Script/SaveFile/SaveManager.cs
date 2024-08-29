using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

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
