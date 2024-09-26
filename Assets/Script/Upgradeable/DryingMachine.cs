// using UnityEngine;

// public class DryingMachine : MonoBehaviour
// {
//     public float dryingSpeed = 1.0f;
//     public IroningStation ironingMachine; // Reference to the ironing machine

//     private int wetClothesCount = 0;
//     private bool isDrying = false;
//     private float dryingTimer = 0.0f;

//     public void AddWetClothes(int count)
//     {
//         wetClothesCount += count;
//         StartDrying();
//     }

//     private void StartDrying()
//     {
//         if (wetClothesCount > 0 && !isDrying)
//         {
//             isDrying = true;
//             dryingTimer = 8.0f; // Time required to dry the clothes
//             Debug.Log("Started drying " + wetClothesCount + " clothes.");
//         }
//     }

//     private void Update()
//     {
//         if (isDrying)
//         {
//             dryingTimer -= Time.deltaTime * dryingSpeed;
//             if (dryingTimer <= 0)
//             {
//                 FinishDrying();
//             }
//         }
//     }

//     private void FinishDrying()
//     {
//         isDrying = false;
//         ironingMachine.AddDryClothes(wetClothesCount);
//         Debug.Log("Finished drying. Moved " + wetClothesCount + " clothes to the ironing machine.");
//         wetClothesCount = 0;
//     }
// }
using UnityEngine;
using UnityEngine.UI;

public class DryingMachine : MonoBehaviour
{
    public float dryingSpeed = 1.0f;
    public IroningStation ironingMachine; // Reference to the ironing machine
    public GameObject progressBarPrefab;  // Prefab of the background and fill image
    public Canvas worldCanvas;            // World-space canvas
    private GameObject progressBarInstance;
    private Image progressBarFillImage;   // Image component with fill type

    private int wetClothesCount = 0;
    private bool isDrying = false;
    private float dryingTimer = 0.0f;

    public void AddWetClothes(int count)
    {
        wetClothesCount += count;
        StartDrying();
    }

    private void StartDrying()
    {
        if (wetClothesCount > 0 && !isDrying)
        {
            isDrying = true;
            dryingTimer = 8.0f; // Time required to dry the clothes
            Debug.Log("Started drying " + wetClothesCount + " clothes.");

            // Instantiate progress bar and attach to the world canvas
            progressBarInstance = Instantiate(progressBarPrefab, worldCanvas.transform);
            progressBarInstance.transform.position = transform.position + Vector3.up * 2;

            // Find the Image with the fill type in the instantiated prefab
            progressBarFillImage = progressBarInstance.transform.Find("Fill").GetComponent<Image>();

            // Start with the bar empty
            progressBarFillImage.fillAmount = 0.0f;
        }
    }

    private void Update()
    {
        if (isDrying)
        {
            dryingTimer -= Time.deltaTime * dryingSpeed;

            // Update the progress bar
            if (progressBarFillImage != null)
            {
                progressBarFillImage.fillAmount = (8.0f - dryingTimer) / 8.0f;  // Update based on timer
            }

            if (dryingTimer <= 0)
            {
                FinishDrying();
            }
        }
    }

    private void FinishDrying()
    {
        isDrying = false;

        // Destroy the progress bar when drying is done
        if (progressBarInstance != null)
        {
            Destroy(progressBarInstance);
        }

        ironingMachine.AddDryClothes(wetClothesCount);
        Debug.Log("Finished drying. Moved " + wetClothesCount + " clothes to the ironing machine.");
        wetClothesCount = 0;
    }
}
