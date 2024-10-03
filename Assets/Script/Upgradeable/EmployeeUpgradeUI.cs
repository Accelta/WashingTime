using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeUpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI speedUpgradeCostText; // Text untuk menampilkan biaya upgrade kecepatan
    public Button upgradeButton;
    public Button upgradeSpeedButton; // Tombol untuk upgrade kecepatan
    public Image[] levelIndicators; // Array of UI Images representing the level indicators untuk karyawan
    public Image[] speedLevelIndicators; // Array of UI Images representing the level indicators untuk kecepatan
    public Sprite blueBubble;
    public Sprite greyBubble;

    private int currentLevel = 0;
    private int[] upgradeCosts = { 8000, 12000, 16000 }; // Biaya untuk setiap level upgrade karyawan
    private float[] speedLevels = { 2.5f, 2.7f, 2.8f, 3.0f }; // Kecepatan berjalan untuk setiap level
    private int currentSpeedLevel = 0;
    private int[] speedUpgradeCosts = { 6000, 8000, 10000 }; // Biaya untuk upgrade kecepatan berjalan

    public EmployeeManager employeeManager;
    public static EmployeeUpgradeUI instance;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject); // Removed
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    public void OnUpgradeButtonClicked()
    {
        if (currentLevel < 3 && MoneyManager.instance.currency >= upgradeCosts[currentLevel])
        {
            MoneyManager.instance.SpendCurrency(upgradeCosts[currentLevel]);
            employeeManager.AddEmployee(); // Tambahkan karyawan baru
            currentLevel++;
            UpdateUI();
        }
    }

    public void OnUpgradeSpeedButtonClicked()
    {
        if (currentSpeedLevel < speedLevels.Length - 1 && MoneyManager.instance.currency >= speedUpgradeCosts[currentSpeedLevel])
        {
            MoneyManager.instance.SpendCurrency(speedUpgradeCosts[currentSpeedLevel]);
            currentSpeedLevel++;

            float speedIncrease = speedLevels[currentSpeedLevel] - speedLevels[currentSpeedLevel - 1];
            employeeManager.UpgradeAllEmployeesSpeed(speedIncrease); // Tambahkan kenaikan kecepatan pada semua karyawan
            
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        // Update UI untuk level karyawan
        if (currentLevel < 3)
        {
            upgradeCostText.text = "Upgrade: " + upgradeCosts[currentLevel];
            upgradeButton.interactable = MoneyManager.instance.currency >= upgradeCosts[currentLevel];
        }
        else
        {
            upgradeCostText.text = "Max Level";
            upgradeButton.interactable = false;
        }

        // Update UI untuk upgrade kecepatan
        if (currentSpeedLevel < speedLevels.Length - 1)
        {
            if (currentLevel > 0)
            {
                speedUpgradeCostText.text = "Upgrade Speed: " + speedUpgradeCosts[currentSpeedLevel];
                upgradeSpeedButton.interactable = MoneyManager.instance.currency >= speedUpgradeCosts[currentSpeedLevel];
            }
            else
            {
                speedUpgradeCostText.text = "Add Employees First!";
                upgradeSpeedButton.interactable = false;
            }
        }
        else
        {
            speedUpgradeCostText.text = "Max Speed";
            upgradeSpeedButton.interactable = false;
        }

        // Update bubble indikator level karyawan
        for (int i = 0; i < levelIndicators.Length; i++)
        {
            levelIndicators[i].sprite = i < currentLevel ? blueBubble : greyBubble;
        }

        // Update bubble indikator level kecepatan
        for (int i = 0; i < speedLevelIndicators.Length; i++)
        {
            speedLevelIndicators[i].sprite = i < currentSpeedLevel ? blueBubble : greyBubble;
        }
    }

    public int GetCurrentSpeedLevel()
    {
        return currentSpeedLevel;
    }

public void SetCurrentSpeedLevel(int level)
{
    // Reset speed level to 0 first
    currentSpeedLevel = 0;
    float totalSpeedIncrease = 0f;

    // Calculate the total speed increase based on the desired level
    for (int i = 1; i <= level; i++)
    {
        totalSpeedIncrease += speedLevels[i] - speedLevels[i - 1];
    }

    // Apply the cumulative speed increase
    employeeManager.UpgradeAllEmployeesSpeed(totalSpeedIncrease);

    // Update the current speed level
    currentSpeedLevel = level;
}

}
