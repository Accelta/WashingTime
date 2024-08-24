using UnityEngine;

public class UpgradeEmployeeTrigger : MonoBehaviour
{
    public GameObject upgradePanel; // Panel UI untuk membeli/mengupgrade karyawan
    private bool isPlayerInRange = false;

    void Start()
    {
        upgradePanel.SetActive(false); // Sembunyikan panel di awal
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgradePanel.SetActive(true);
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgradePanel.SetActive(false);
            isPlayerInRange = false;
        }
    }
    public void exit(){
            upgradePanel.SetActive(false);
            isPlayerInRange = false;

    }
}
