using UnityEngine;
using UnityEngine.EventSystems;

public class WashButton : MonoBehaviour, IPointerClickHandler
{
    public ClothesManager clothesManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Find all clothes in the scene
        ClothesStep[] clothesArray = FindObjectsOfType<ClothesStep>();
        
        foreach (ClothesStep clothes in clothesArray)
        {
            // Only wash clothes that are not already clean
            if (clothes.IsReadyToWash())
            {
                clothes.Wash();
                clothesManager.ShowNextClothes();
            }
        }
    }
}
