using System.Collections;
using System.Collections.Generic;
using Script.Interface;
using Script.SO;
using UnityEngine;

public class PickUpBaju : MonoBehaviour, Iinteractable
{
    [SerializeField] private GameObject interactionClueUI;

    public BajuSo bajuSo;

    private void Start()
    {
        if (bajuSo == null)
        {
            Debug.LogWarning("Baju SO kosong");
        }
        interactionClueUI.SetActive(false);
    }
    public void Interact()
    {
        if (GameManager.Instance.TryPickup(bajuSo))
        {
            Destroy(gameObject);
        }
        else
        {
            print("Baju Masih Ada");
        }

    }

    private void OnDestroy()
    {
        interactionClueUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionClueUI.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionClueUI.SetActive(false);
        }
    }
        public GameObject GetInteractionClueUI()
    {
        return interactionClueUI;
    }
}
