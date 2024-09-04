using UnityEngine;

public class IroningStation : MonoBehaviour
{
    public float ironingSpeed = 1.0f;
    public CleanClothesArea cleanClothesArea; // Reference to the clean clothes area

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
        }
    }

    private void Update()
    {
        if (isIroning)
        {
            ironingTimer -= Time.deltaTime * ironingSpeed;
            if (ironingTimer <= 0)
            {
                FinishIroning();
            }
        }
    }

    private void FinishIroning()
    {
        isIroning = false;
        cleanClothesArea.AddCleanClothes(dryClothesCount);
        Debug.Log("Finished ironing. Added " + dryClothesCount + " clothes to the clean clothes area.");
        dryClothesCount = 0;
    }
}
