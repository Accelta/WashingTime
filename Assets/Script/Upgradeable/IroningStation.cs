// using UnityEngine;

// public class IroningStation : MonoBehaviour
// {
//     public float ironingSpeed = 1.0f;
//     public CleanClothesArea cleanClothesArea; // Reference to the clean clothes area

//     private int dryClothesCount = 0;
//     private bool isIroning = false;
//     private float ironingTimer = 0.0f;

//     public void AddDryClothes(int count)
//     {
//         dryClothesCount += count;
//         StartIroning();
//     }

//     private void StartIroning()
//     {
//         if (dryClothesCount > 0 && !isIroning)
//         {
//             isIroning = true;
//             ironingTimer = 6.0f; // Time required to iron the clothes
//             Debug.Log("Started ironing " + dryClothesCount + " clothes.");
//         }
//     }

//     private void Update()
//     {
//         if (isIroning)
//         {
//             ironingTimer -= Time.deltaTime * ironingSpeed;
//             if (ironingTimer <= 0)
//             {
//                 FinishIroning();
//             }
//         }
//     }

//     private void FinishIroning()
//     {
//         isIroning = false;
//         cleanClothesArea.AddCleanClothes(dryClothesCount);
//         Debug.Log("Finished ironing. Added " + dryClothesCount + " clothes to the clean clothes area.");
//         dryClothesCount = 0;
//     }
// }
using UnityEngine;
using UnityEngine.UI;

public class IroningStation : MonoBehaviour
{
    public float ironingSpeed = 1.0f;
    public CleanClothesArea cleanClothesArea; // Reference to the clean clothes area
    public GameObject progressBarPrefab;      // Prefab of the background and fill image
    public Canvas worldCanvas;                // World-space canvas
    private GameObject progressBarInstance;
    private Image progressBarFillImage;       // Image component with fill type

    private int dryClothesCount = 0;
    private bool isIroning = false;
    private float ironingTimer = 0.0f;

    public void AddDryClothes(int count)
    {
        dryClothesCount += count;
        StartIroning();
    }

    private void StartIroning()
    {
        if (dryClothesCount > 0 && !isIroning)
        {
            isIroning = true;
            ironingTimer = 6.0f; // Time required to iron the clothes
            Debug.Log("Started ironing " + dryClothesCount + " clothes.");

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
        if (isIroning)
        {
            ironingTimer -= Time.deltaTime * ironingSpeed;

            // Update the progress bar
            if (progressBarFillImage != null)
            {
                progressBarFillImage.fillAmount = (6.0f - ironingTimer) / 6.0f;  // Update based on timer
            }

            if (ironingTimer <= 0)
            {
                FinishIroning();
            }
        }
    }

    private void FinishIroning()
    {
        isIroning = false;

        // Destroy the progress bar when ironing is done
        if (progressBarInstance != null)
        {
            Destroy(progressBarInstance);
        }

        cleanClothesArea.AddCleanClothes(dryClothesCount);
        Debug.Log("Finished ironing. Added " + dryClothesCount + " clothes to the clean clothes area.");
        dryClothesCount = 0;
    }
}
