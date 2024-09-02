using UnityEngine;

public class LaundryBasket : MonoBehaviour
{
    public int capacity = 5;
    public int currentDirtyClothes = 0;

    public bool AddDirtyClothes()
    {
        if (currentDirtyClothes < capacity)
        {
            currentDirtyClothes++;
            Debug.Log("Dirty clothes added. Current count: " + currentDirtyClothes);
            return true;
        }
        else
        {
            Debug.Log("Basket is full!");
            return false;
        }
    }

    public int TakeDirtyClothes(int amount)
    {
        int clothesToTake = Mathf.Min(amount, currentDirtyClothes);
        currentDirtyClothes -= clothesToTake;
        return clothesToTake;
    }

    public bool IsFull()
    {
        return currentDirtyClothes >= capacity;
    }
}
