using Unity.VisualScripting;
using UnityEngine;

public class UpgradeEmployeeTrigger : MonoBehaviour
{
    public GameObject upgradePanel; // Panel UI untuk membeli/mengupgrade karyawan
    private bool isPlayerInRange = false;

    void Start()
    {
        upgradePanel.gameObject.GetComponent<Canvas>().enabled =false; // Sembunyikan panel di awal
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        upgradePanel.gameObject.GetComponent<Canvas>().enabled =true;
        isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgradePanel.gameObject.GetComponent<Canvas>().enabled =false;
            isPlayerInRange = false;
        }
    }
    public void exit(){
            upgradePanel.gameObject.GetComponent<Canvas>().enabled =false;
            isPlayerInRange = false;

    }
}
