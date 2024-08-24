using UnityEngine;

public class UpgradeTrigger : MonoBehaviour
{
    public WashingMachine washingMachine;
    public GameObject upgradeUIPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgradeUIPanel.SetActive(true);
            upgradeUIPanel.GetComponent<UpgradeUI>().Setup(washingMachine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgradeUIPanel.SetActive(false);
        }
    }
    public void exit(){
    upgradeUIPanel.SetActive(false);

    }
}
