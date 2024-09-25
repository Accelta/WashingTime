using TMPro;
using UnityEngine;

public class WashingMachine : MonoBehaviour, IWashable
{
    public float washSpeed = 1.0f;
    public float upgradeAmount = 0.5f;
    public int upgradeCost = 50;
    public bool isUnlocked = false;
    public GameObject[] washingMachineLevels; // Array to hold the different levels of washing machines (drag the child objects in the inspector)
    public GameObject levelBubblePrefab;
    public GameObject upgradeArea; // Assign the upgrade area specific to this machine in the editor

    private int dirtyClothesCount = 0;
    private float washTimer = 0.0f;
    public bool isWashing = false;
    private CleanClothesArea cleanClothesArea;
    private int upgradeLevel = 0; // Start at level 0
    private GameObject levelBubble;

    public float WashSpeed { get => washSpeed; set => washSpeed = value; }
    public float UpgradeAmount { get => upgradeAmount; }
    public int UpgradeCost { get => upgradeCost; set => upgradeCost = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }
    public int UpgradeLevel { get => upgradeLevel; } // Public getter for UpgradeLevel
    public DryingMachine dryingMachine;
    private AudioSource audioSource;

    private void Start()
    {
        cleanClothesArea = FindObjectOfType<CleanClothesArea>();
        
        // Ensure that the machine is hidden if it's not unlocked
        if (!isUnlocked)
        {
            HideWashingMachine();
        }
        else
        {
            UpdateMachineModel();
        }

        CreateLevelBubble();
        audioSource = GetComponent<AudioSource>();
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
             if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.loop = true; // Ensure it loops during washing
                audioSource.Play();
            }
        }
    }

    private void FinishWashing()
    {
        isWashing = false;
        dryingMachine.AddWetClothes(dirtyClothesCount);
        Debug.Log("Finished washing. Moved " + dirtyClothesCount + " clothes to the drying machine.");
        dirtyClothesCount = 0;
         if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void Upgrade()
    {
        if (upgradeLevel >= washingMachineLevels.Length - 1)
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
            UpdateMachineModel(); // Switch to the new level's model
            UpdateLevelBubble();
            Debug.Log("Washing machine upgraded! New wash speed: " + washSpeed + ", next upgrade cost: " + upgradeCost);

            if (upgradeLevel >= washingMachineLevels.Length - 1 && upgradeArea != null)
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
        ShowWashingMachine(); // Show the washing machine once unlocked
        Debug.Log("Washing machine unlocked!");
    }

    private void UpdateMachineModel()
    {
        // Disable all washing machine levels
        foreach (GameObject machine in washingMachineLevels)
        {
            machine.SetActive(false);
        }

        // Enable the machine corresponding to the current upgrade level
        if (upgradeLevel < washingMachineLevels.Length)
        {
            washingMachineLevels[upgradeLevel].SetActive(true);
        }
        else
        {
            Debug.LogError("Upgrade level exceeds available machine models.");
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
        return upgradeLevel < washingMachineLevels.Length - 1;
    }

    // Hide all washing machine models when locked
    private void HideWashingMachine()
    {
        foreach (GameObject machine in washingMachineLevels)
        {
            machine.SetActive(false);
        }
    }

    // Show the machine model once unlocked
    private void ShowWashingMachine()
    {
        UpdateMachineModel(); // This will enable the correct machine model
    }
}
