using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableClothes : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public enum EquipmentType { Soap, Bleach, Softener, Brush, Rinse }

    public EquipmentType equipmentType;

    private RectTransform rectTransform;
    private Canvas canvas;
    private ClothesStep clothes;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if the object is dropped over a clothes item
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            Debug.Log("Dropped on: " + eventData.pointerCurrentRaycast.gameObject.name);
            if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Clothes"))
            {
                clothes = eventData.pointerCurrentRaycast.gameObject.GetComponent<ClothesStep>();
                if (clothes != null)
                {
                    ApplyStatusEffect();
                }
                else
                {
                    Debug.LogError("Clothes component not found on the target object.");
                }
            }
            else
            {
                Debug.LogError("Dragged object did not hit a Clothes tagged object.");
            }
        }
        else
        {
            Debug.LogError("Dragged object did not hit any object.");
        }
    }

    private void ApplyStatusEffect()
    {
        if (clothes != null)
        {
            switch (equipmentType)
            {
                case EquipmentType.Soap:
                    clothes.ApplyStatus(ClothesStep.Status.Soapy);
                    Debug.Log("Soapy");
                    break;
                case EquipmentType.Bleach:
                    clothes.ApplyStatus(ClothesStep.Status.Bleached);
                    Debug.Log("Bleached");
                    break;
                case EquipmentType.Softener:
                    clothes.ApplyStatus(ClothesStep.Status.Softened);
                    Debug.Log("Softened");
                    break;
                case EquipmentType.Brush:
                    clothes.ApplyStatus(ClothesStep.Status.Brushed);
                    Debug.Log("Brushed");
                    break;
                case EquipmentType.Rinse:
                    clothes.ApplyStatus(ClothesStep.Status.Rinse);
                    Debug.Log("Rinse");
                    break;
                default:
                    Debug.LogError("Unknown EquipmentType.");
                    break;
            }
        }
    }
}
