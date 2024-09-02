using UnityEngine;

public class Player : MonoBehaviour
{
    public int dirtyClothesHolding = 0;
    public int maxDirtyClothes = 5;
    public float interactionRange = 2.0f; // Range for automatic interaction

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("LaundryBasket"))
            {
                LaundryBasket basket = hitCollider.GetComponent<LaundryBasket>();
                if (basket != null)
                {
                    TakeDirtyClothes(basket);
                }
            }
            else if (hitCollider.CompareTag("WashingMachine"))
            {
                WashingMachine machine = hitCollider.GetComponent<WashingMachine>();
                if (machine != null)
                {
                    PutClothesInWashingMachine(machine);
                }
            }
        }
    }

    private void TakeDirtyClothes(LaundryBasket basket)
    {
        int clothesToTake = Mathf.Min(maxDirtyClothes - dirtyClothesHolding, basket.TakeDirtyClothes(maxDirtyClothes - dirtyClothesHolding));
        if (clothesToTake > 0)
        {
            dirtyClothesHolding += clothesToTake;
            Debug.Log("Took " + clothesToTake + " dirty clothes. Now holding: " + dirtyClothesHolding);
        }
    }

    private void PutClothesInWashingMachine(WashingMachine machine)
    {
        if (dirtyClothesHolding > 0 && machine.AddDirtyClothes(dirtyClothesHolding))
        {
            dirtyClothesHolding = 0;
            Debug.Log("Put all dirty clothes in the washing machine.");
        }
    }
}
