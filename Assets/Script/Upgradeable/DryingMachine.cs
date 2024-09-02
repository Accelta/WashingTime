using UnityEngine;

public class DryingMachine : MonoBehaviour
{
    public float drySpeed = 1.0f;
    private int dirtyClothesCount = 0;
    private float dryTimer = 0.0f;
    public bool isDrying = false;
    private IroningStation ironingStation;

    private void Start()
    {
        ironingStation = FindObjectOfType<IroningStation>();
    }

    private void Update()
    {
        if (isDrying)
        {
            dryTimer -= Time.deltaTime * drySpeed;
            if (dryTimer <= 0)
            {
                FinishDrying();
            }
        }
    }

    public void AddClothes(int count)
    {
        dirtyClothesCount += count;
        StartDrying();
    }

    private void StartDrying()
    {
        if (dirtyClothesCount > 0)
        {
            isDrying = true;
            dryTimer = 10.0f; // Time to dry
            Debug.Log("Started drying " + dirtyClothesCount + " clothes.");
        }
    }

    private void FinishDrying()
    {
        isDrying = false;
        ironingStation.AddClothes(dirtyClothesCount);
        Debug.Log("Finished drying. Transferred " + dirtyClothesCount + " clothes to ironing station.");
        dirtyClothesCount = 0;
    }
}
