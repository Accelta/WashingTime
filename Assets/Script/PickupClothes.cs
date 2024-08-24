using UnityEngine;
using UnityEngine.InputSystem;

public class PickupClothes : MonoBehaviour
{
private bool isPlayerInside = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            isPlayerInside = true;
        }
    }

    private void PickUp()
    {
        if (Inventory.instance.AddClothes(gameObject))
        {
            Debug.Log("Clothes picked up");
            gameObject.SetActive(false); // Hide the clothes in the scene
        }
        else
        {
            Debug.Log("Inventory full");
        }
    }

  private void Update()
    {
        // Check if the player clicks left mouse button when inside the water bucket collider
        if (Input.GetMouseButtonDown(0) && isPlayerInside)
        {
                PickUp();    
        }
    }

}
