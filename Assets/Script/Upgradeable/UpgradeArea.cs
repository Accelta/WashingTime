using UnityEngine;

public class UpgradeArea : MonoBehaviour
{
    public MonoBehaviour washableDevice;  // Referensi ke alat cuci yang akan di-upgrade atau dibuka
    private IWashable washable;

    private void Start()
    {
        washable = washableDevice as IWashable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryUpgradeOrUnlock();
        }
    }

    private void TryUpgradeOrUnlock()
    {
        int upgradeCost = washable.UpgradeCost;
        int playerCurrency = MoneyManager.instance.currency;

        if (!washable.IsUnlocked)
        {
            if (playerCurrency >= upgradeCost)
            {
                MoneyManager.instance.SpendCurrency(upgradeCost);
                washable.Unlock();
            }
            else if (playerCurrency > 0)
            {
                MoneyManager.instance.SpendCurrency(playerCurrency);
                washable.UpgradeCost -= playerCurrency;
                Debug.Log("Not enough currency for unlocking. Remaining cost: " + washable.UpgradeCost);
            }
            else
            {
                Debug.Log("Not enough currency to unlock.");
            }
        }
        else
        {
            if (playerCurrency >= upgradeCost)
            {
                MoneyManager.instance.SpendCurrency(upgradeCost);
                washable.Upgrade();
            }
            else if (playerCurrency > 0)
            {
                MoneyManager.instance.SpendCurrency(playerCurrency);
                washable.UpgradeCost -= playerCurrency;
                Debug.Log("Not enough currency for full upgrade. Remaining cost: " + washable.UpgradeCost);
            }
            else
            {
                Debug.Log("Not enough currency to upgrade.");
            }
        }
    }
}
