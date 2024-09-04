using UnityEngine;

public class DryingMachine : MonoBehaviour
{
    public float dryingSpeed = 1.0f;
    public IroningStation ironingMachine; // Reference to the ironing machine

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
        }
    }

    private void Update()
    {
        if (isDrying)
        {
            dryingTimer -= Time.deltaTime * dryingSpeed;
            if (dryingTimer <= 0)
            {
                FinishDrying();
            }
        }
    }

    private void FinishDrying()
    {
        isDrying = false;
        ironingMachine.AddDryClothes(wetClothesCount);
        Debug.Log("Finished drying. Moved " + wetClothesCount + " clothes to the ironing machine.");
        wetClothesCount = 0;
    }
}
