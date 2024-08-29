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
    private int[] upgradeCosts = { 80000, 120000, 160000 }; // Biaya untuk setiap level upgrade karyawan
    private float[] speedLevels = { 2.5f, 2.7f, 2.8f, 3.0f }; // Kecepatan berjalan untuk setiap level
    private int currentSpeedLevel = 0;
    private int[] speedUpgradeCosts = { 60000, 80000, 100000 }; // Biaya untuk upgrade kecepatan berjalan

    public EmployeeManager employeeManager;
    public static EmployeeUpgradeUI instance;

        private void Awake()
    {
        // Singleton pattern
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

    private void UpdateUI()
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
        // Cek apakah ada karyawan (currentLevel > 0)
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
        if (i < currentLevel)
        {
            levelIndicators[i].sprite = blueBubble;
        }
        else
        {
            levelIndicators[i].sprite = greyBubble;
        }
    }

    // Update bubble indikator level kecepatan
    for (int i = 0; i < speedLevelIndicators.Length; i++)
    {
        if (i < currentSpeedLevel)
        {
            speedLevelIndicators[i].sprite = blueBubble;
        }
        else
        {
            speedLevelIndicators[i].sprite = greyBubble;
        }
    }
}

public int GetCurrentSpeedLevel()
{
    return currentSpeedLevel; // Assuming you have a currentSpeedLevel variable
}

public void SetCurrentSpeedLevel(int level)
{
    currentSpeedLevel = level;
    employeeManager.UpgradeAllEmployeesSpeed(speedLevels[currentSpeedLevel]);
}
}
