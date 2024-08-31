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
        if (currentMachine.UpgradeLevel < 3 && MoneyManager.instance.currency >= currentMachine.UpgradeCost)
        {
            currentMachine.Upgrade();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (currentMachine.UpgradeLevel < 3)
        {
            upgradeCostText.text = "Upgrade : " + currentMachine.UpgradeCost;
            upgradeButton.interactable = MoneyManager.instance.currency >= currentMachine.UpgradeCost;
        }
        else
        {
            upgradeCostText.text = "Max Level";
            upgradeButton.interactable = false;
        }

        for (int i = 0; i < levelIndicators.Length; i++)
        {
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
