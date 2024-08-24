using System.Collections;
using System.Collections.Generic;
using Script.Interface;
using UnityEngine;

public class BajuItem : MonoBehaviour, Iinteractable
{
    [SerializeField] private GameObject interactionClueUI;
    [SerializeField] public AudioClip suaraAmbilClip;
    [SerializeField] public AudioClip suaraAmbil2Clip;

    private void Start()
    {

        interactionClueUI.SetActive(false); // Ensure the clue UI is hidden initially

    }

    public void Interact()
    {
        GameManager.Instance.AddBaju();
        Destroy(gameObject);
        interactionClueUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the interaction clue UI
            interactionClueUI.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide the interaction clue UI

            interactionClueUI.SetActive(false);

        }
    }
    void OnDestroy()
    {
        interactionClueUI.SetActive(false);

    }
}
