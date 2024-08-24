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
        UpdateCurrencyText();
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
        UpdateCurrencyText();
    }

    public void SpendCurrency(int amount)
    {
        currency -= amount;
        UpdateCurrencyText();
    }

    private void UpdateCurrencyText()
    {
        currencyText.text = FormatCurrency(currency);
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
