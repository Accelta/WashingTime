
using System.Collections;
using System.Collections.Generic;
using Script.Interface;
using Script.SO;
using Unity.VisualScripting;
using UnityEngine;

public class BajuKeranjang : MonoBehaviour, Iinteractable
{
    public BajuType bajuType;
    private Inventory inventory;
    public AudioClip onCorrectClip;
    [SerializeField] private PickUpBaju pickupbaju;
    [SerializeField] private BajuDragAble bajuDragAble;

    void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        // Reinitialize when the object becomes enabled
        Initialize();
    }

    private void Initialize()
    {
        inventory = GameManager.Instance?.Inventory;

        if (bajuDragAble == null)
        {
            bajuDragAble = FindObjectOfType<BajuDragAble>();
        }
    }

    public void Interact()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory is null in BajuKeranjang.Interact");
            return;
        }

        if (inventory.CurrectBajuSo == null) return;

        if (inventory.CurrectBajuSo.Bajutype == bajuType)
        {
            AudioSource.PlayClipAtPoint(onCorrectClip, Camera.main.transform.position);
            inventory.ResetBaju();
        }
        else
        {
            print("Salah baju");
            AudioSource.PlayClipAtPoint(bajuDragAble.GetSalahClip(), Camera.main.transform.position);
            GameManager.Instance.DialogueManager.StartDialogue("Ooops... sepertinya ada yang salah", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && pickupbaju != null)
        {
            pickupbaju.GetInteractionClueUI().SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && pickupbaju != null)
        {
            pickupbaju.GetInteractionClueUI().SetActive(false);
        }
    }
}
