using UnityEngine;

public class Tub : MonoBehaviour, IWashable
{
    public float washSpeed = 1.0f;
    public float upgradeAmount = 0.5f;
    public int upgradeCost = 50;
    public bool isUnlocked = false;

    public float WashSpeed { get => washSpeed; set => washSpeed = value; }
    public float UpgradeAmount { get => upgradeAmount; }
    public int UpgradeCost { get => upgradeCost; set => upgradeCost = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }

    public void Upgrade()
    {
        washSpeed += upgradeAmount;
        Debug.Log("Tub upgraded! New wash speed: " + washSpeed);
    }

    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("Tub unlocked!");
    }
}
