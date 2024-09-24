using UnityEngine;

public class UpgradeArea : MonoBehaviour
{
    public MonoBehaviour washableDevice;  // Reference to the washable device (WashingMachine)
    private IWashable washable;
    public UpgradeUI upgradeUI; // Reference to the UpgradeUI script to update UI

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
                upgradeUI.Setup(washableDevice as WashingMachine); // Refresh UI after unlocking
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
                upgradeUI.Setup(washableDevice as WashingMachine); // Refresh UI after upgrade
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
