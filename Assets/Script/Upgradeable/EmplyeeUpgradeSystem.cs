using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeUpgradeSystem : MonoBehaviour
{
    public TextMeshProUGUI upgradeText; // Teks yang menampilkan harga upgrade
    public Button upgradeButton; // Tombol untuk membeli/upgrade karyawan
    public EmployeeManager employeeManager; // Referensi ke EmployeeManager

    private int[] upgradeCosts = { 80000, 120000, 160000 }; // Harga untuk setiap level
    private int currentLevel = 0;

    void Start()
    {
        UpdateUpgradeUI();
        upgradeButton.onClick.AddListener(UpgradeEmployee);
    }

    void UpdateUpgradeUI()
    {
        if (currentLevel < upgradeCosts.Length)
        {
            upgradeText.text = "Upgrade Employee: $" + upgradeCosts[currentLevel];
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeText.text = "Max level reached!";
            upgradeButton.interactable = false;
        }
    }

    public void UpgradeEmployee()
    {
        if (currentLevel < upgradeCosts.Length && MoneyManager.instance.currency >= upgradeCosts[currentLevel])
        {
            MoneyManager.instance.SpendCurrency(upgradeCosts[currentLevel]);
            employeeManager.AddEmployee(); // Tambahkan karyawan baru
            currentLevel++;
            UpdateUpgradeUI();
        }
        else
        {
            Debug.Log("Not enough money or max level reached.");
        }
    }
}

