using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI upgradeCostText;
    public Button upgradeButton;
    public Image[] levelIndicators; // Array of UI Images representing the level indicators
    public Sprite blueBubble;
    public Sprite greyBubble;
    private WashingMachine currentMachine;

    public void Setup(WashingMachine machine)
    {
        currentMachine = machine;
        UpdateUI();
    }

    public void OnUpgradeButtonClicked()
    {
        if (!currentMachine.IsUnlocked) // If the machine is locked, trigger unlock
        {
            TryUnlock();
        }
        else if (currentMachine.UpgradeLevel < 3 && MoneyManager.instance.currency >= currentMachine.UpgradeCost)
        {
            currentMachine.Upgrade();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (!currentMachine.IsUnlocked)
        {
            // Machine is locked, show "Unlock" text and hide level indicators
            upgradeCostText.text = "Unlock: " + currentMachine.UpgradeCost;
            upgradeButton.interactable = MoneyManager.instance.currency >= currentMachine.UpgradeCost;
            
            // Disable level indicators (e.g. gray them out)
            foreach (Image indicator in levelIndicators)
            {
                indicator.gameObject.SetActive(false); // Hide the level indicators when locked
            }
        }
        else
        {
            // Machine is unlocked, update the upgrade UI
            if (currentMachine.UpgradeLevel < 3)
            {
                upgradeCostText.text = "Upgrade: " + currentMachine.UpgradeCost;
                upgradeButton.interactable = MoneyManager.instance.currency >= currentMachine.UpgradeCost;
            }
            else
            {
                upgradeCostText.text = "Max Level";
                upgradeButton.interactable = false;
            }

            // Update level indicators
            for (int i = 0; i < levelIndicators.Length; i++)
            {
                levelIndicators[i].gameObject.SetActive(true); // Show the indicators
                if (i < currentMachine.UpgradeLevel)
                {
                    levelIndicators[i].sprite = blueBubble;
                }
                else
                {
                    levelIndicators[i].sprite = greyBubble;
                }
            }
        }
    }

    private void TryUnlock()
    {
        if (MoneyManager.instance.currency >= currentMachine.UpgradeCost)
        {
            MoneyManager.instance.SpendCurrency(currentMachine.UpgradeCost);
            currentMachine.Unlock();
            UpdateUI(); // Refresh UI after unlocking
        }
    }
}
