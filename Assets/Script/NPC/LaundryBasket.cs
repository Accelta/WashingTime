// using UnityEngine;

// public class LaundryBasket : MonoBehaviour
// {
//     public int capacity = 5;
//     public int currentDirtyClothes = 0;

//     public bool AddDirtyClothes()
//     {
//         if (currentDirtyClothes < capacity)
//         {
//             currentDirtyClothes++;
//             Debug.Log("Dirty clothes added. Current count: " + currentDirtyClothes);
//             return true;
//         }
//         else
//         {
//             Debug.Log("Basket is full!");
//             return false;
//         }
//     }

//     public int TakeDirtyClothes(int amount)
//     {
//         int clothesToTake = Mathf.Min(amount, currentDirtyClothes);
//         currentDirtyClothes -= clothesToTake;
//         return clothesToTake;
//     }

//     public bool IsFull()
//     {
//         return currentDirtyClothes >= capacity;
//     }
// }
using UnityEngine;

public class LaundryBasket : MonoBehaviour
{
    public int capacity = 5;
    public int currentDirtyClothes = 0;
    public GameObject dirtyClothesObject; // Object to spawn/disable

    void Start()
    {
        // Ensure the dirtyClothesObject is initially hidden if there are no dirty clothes
        if (currentDirtyClothes == 0 && dirtyClothesObject != null)
        {
            dirtyClothesObject.SetActive(false);
        }
    }

    public bool AddDirtyClothes()
    {
        if (currentDirtyClothes < capacity)
        {
            currentDirtyClothes++;
            Debug.Log("Dirty clothes added. Current count: " + currentDirtyClothes);

            // Enable object if this is the first dirty cloth added
            if (currentDirtyClothes > 0 && dirtyClothesObject != null)
            {
                dirtyClothesObject.SetActive(true);
            }
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

        // If no more dirty clothes, deactivate the object
        if (currentDirtyClothes == 0 && dirtyClothesObject != null)
        {
            dirtyClothesObject.SetActive(false);
        }

        return clothesToTake;
    }

    public bool IsFull()
    {
        return currentDirtyClothes >= capacity;
    }
}

