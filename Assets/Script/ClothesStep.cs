using UnityEngine;
using UnityEngine.UI;

public class ClothesStep : MonoBehaviour
{
    public enum Status { Dirty, Soapy, Bleached, Brushed, Softened, Rinse, Clean }

    private Status currentStatus = Status.Dirty;
    public ClothesDataScript clothesData;
    private Image imageComponent;
    private int currentStepIndex = 0;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        UpdateSprite();
    }

    public void ApplyStatus(Status newStatus)
    {
        // Ensure the new status follows the washing sequence
        if (IsValidStatusTransition(newStatus))
        {
            currentStatus = newStatus;
            currentStepIndex++;
            UpdateSprite();
            Debug.Log("Clothes status updated to: " + newStatus);
        }
        else
        {
            Debug.LogError("Invalid status transition: " + newStatus);
        }
    }

    public void Wash()
    {
        if (currentStatus != Status.Dirty)
        {
            currentStatus = Status.Clean;
            UpdateSprite();
            Debug.Log("Clothes washed and set to Clean status.");
        }
    }

    public bool IsReadyToWash()
    {
        // Check if the clothes are in the last step of the washing sequence
        return currentStepIndex == clothesData.washingSteps.Length;
    }

    private void UpdateSprite()
    {
        if (clothesData != null && currentStepIndex < clothesData.clothingSprites.Length)
        {
            imageComponent.sprite = clothesData.clothingSprites[currentStepIndex];
        }
        else
        {
            Debug.LogError("Sprite not found or currentStepIndex out of range.");
        }
    }

    private bool IsValidStatusTransition(Status newStatus)
    {
        // Ensure the new status follows the washing sequence defined in clothesData
        if (currentStepIndex < clothesData.washingSteps.Length)
        {
            return clothesData.washingSteps[currentStepIndex].action == newStatus.ToString();
        }
        return false;
    }
}
