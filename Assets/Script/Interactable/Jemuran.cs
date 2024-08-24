using System;
using Script.Interface;
using Script.Jemuran;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Interactable
{
    public class Jemuran : MonoBehaviour, Iinteractable
    {
        public JemuranManager JemuranManager;
        public AudioClip suarajemur;
        [SerializeField] private GameObject interactionClueUI;
        [SerializeField] private WashManager washmanager;


        private bool canInteract = false;
        private void Start()
        {
            interactionClueUI.SetActive(false);

        }
        public void Interact()
        {
            if (JemuranManager._canShowJemuran)

            {
                JemuranManager.gameObject.SetActive(true);
                GameManager.Instance.questManager.HideQuestPanel();
                interactionClueUI.SetActive(false);


            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canInteract = true;
                AudioSource.PlayClipAtPoint(suarajemur, Camera.main.transform.position);
                interactionClueUI.SetActive(true);
                GameManager.Instance.DialogueManager.StartDialogue("Sekarang mari menjemur baju dari yang paling ringan", true);
                GameManager.Instance.questManager.HideQuestPanel();


            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canInteract = false;
            }
        }
    }
}