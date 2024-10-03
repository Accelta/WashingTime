using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public int currency = 0;
    public TextMeshProUGUI currencyText;

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

    private void Start()
    {
        // Ensure the currency text reference is set correctly after a reload
        if (currencyText == null)
        {
            currencyText = FindObjectOfType<TextMeshProUGUI>();
        }
        UpdateCurrencyText();
    }

    private void OnLevelWasLoaded(int level)
    {
        // Re-find and assign the UI element when the scene reloads
        if (currencyText == null)
        {
            currencyText = FindObjectOfType<TextMeshProUGUI>();
        }
        UpdateCurrencyText(); // Update UI with the current value
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
        UpdateCurrencyText();
         EmployeeUpgradeUI.instance?.UpdateUI();
    }

    public void SpendCurrency(int amount)
    {
        currency -= amount;
        UpdateCurrencyText();
        EmployeeUpgradeUI.instance?.UpdateUI();

    }

    private void UpdateCurrencyText()
    {
        if (currencyText != null)
        {
            currencyText.text = FormatCurrency(currency);
        }
    }

    private string FormatCurrency(int amount)
    {
        if (amount >= 1000000)
        {
            return (amount / 1000000f).ToString("0.##M");
        }
        else if (amount >= 10000)
        {
            return (amount / 1000f).ToString("0.##K");
        }
        else
        {
            return amount.ToString();
        }
    }
}
