using TMPro;
using UnityEngine;

public class WashingMachine : MonoBehaviour, IWashable
{
    public float washSpeed = 1.0f;
    public float upgradeAmount = 0.5f;
    public int upgradeCost = 50;
    public bool isUnlocked = false;
    public Texture[] upgradeTextures;
    public GameObject levelBubblePrefab;
    public GameObject upgradeArea; // Assign the upgrade area specific to this machine in the editor

    private int dirtyClothesCount = 0;
    private float washTimer = 0.0f;
    public bool isWashing = false;
    private CleanClothesArea cleanClothesArea;
    private Renderer machineRenderer;
    private int upgradeLevel = 0;
    private GameObject levelBubble;

    public float WashSpeed { get => washSpeed; set => washSpeed = value; }
    public float UpgradeAmount { get => upgradeAmount; }
    public int UpgradeCost { get => upgradeCost; set => upgradeCost = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }
    public int UpgradeLevel { get => upgradeLevel; } // Public getter for UpgradeLevel
    public DryingMachine dryingMachine;

    private void Start()
    {
        cleanClothesArea = FindObjectOfType<CleanClothesArea>();
        machineRenderer = GetComponent<Renderer>();
        UpdateTexture();
        CreateLevelBubble();
    }

    private void Update()
    {
        if (isWashing)
        {
            washTimer -= Time.deltaTime * washSpeed;
            if (washTimer <= 0)
            {
                FinishWashing();
            }
        }
    }

    public bool AddDirtyClothes(int count)
    {
        if (!isWashing)
        {
            dirtyClothesCount += count;
            StartWashing();
            return true;
        }
        else
        {
            Debug.Log("Washing machine is currently in use.");
            return false;
        }
    }

    private void StartWashing()
    {
        if (dirtyClothesCount > 0)
        {
            isWashing = true;
            washTimer = 10.0f; // Waktu mencuci 10 detik
            Debug.Log("Started washing " + dirtyClothesCount + " clothes.");
        }
    }

    // private void FinishWashing()
    // {
    //     isWashing = false;
    //     cleanClothesArea.AddCleanClothes(dirtyClothesCount);
    //     Debug.Log("Finished washing. Added " + dirtyClothesCount + " clean clothes.");
    //     dirtyClothesCount = 0;
    // }
private void FinishWashing()
    {
        isWashing = false;
        dryingMachine.AddWetClothes(dirtyClothesCount);
        Debug.Log("Finished washing. Moved " + dirtyClothesCount + " clothes to the drying machine.");
        dirtyClothesCount = 0;
    }
    public void Upgrade()
    {
        if (upgradeLevel >= 3)
        {
            Debug.Log("Maximum upgrade level reached.");
            if (upgradeArea != null)
            {
                upgradeArea.SetActive(false); // Disable the upgrade area if max level is reached
            }
            return;
        }

        if (MoneyManager.instance.currency >= upgradeCost)
        {
            MoneyManager.instance.SpendCurrency(upgradeCost);
            washSpeed += upgradeAmount;
            upgradeLevel++;
            upgradeCost = Mathf.RoundToInt(upgradeCost * 2.5f); // Increase the cost for the next upgrade
            UpdateTexture();
            UpdateLevelBubble();
            Debug.Log("Washing machine upgraded! New wash speed: " + washSpeed + ", next upgrade cost: " + upgradeCost);

            if (upgradeLevel >= 3 && upgradeArea != null)
            {
                upgradeArea.SetActive(false); // Disable the upgrade area if max level is reached
            }
        }
        else
        {
            Debug.Log("Not enough money to upgrade.");
        }
    }

    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("Washing machine unlocked!");
    }

    private void UpdateTexture()
    {
        if (upgradeLevel < upgradeTextures.Length)
        {
            machineRenderer.material.mainTexture = upgradeTextures[upgradeLevel];
        }
        else
        {
            Debug.Log("Max upgrade level reached. No more textures to apply.");
        }
    }

    private void CreateLevelBubble()
    {
        levelBubble = Instantiate(levelBubblePrefab, transform.position + Vector3.up * 2, Quaternion.identity, transform);
        UpdateLevelBubble();
    }

    private void UpdateLevelBubble()
    {
        if (levelBubble != null)
        {
            levelBubble.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + upgradeLevel;
        }
    }

    public bool CanUpgrade()
    {
        return upgradeLevel < 3;
    }
}
