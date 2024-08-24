using System.Collections;
using System.Collections.Generic;
using Script.Interface;
using StarterAssets;
using UnityEngine;

namespace Script.Interactable
{
    public class Book : MonoBehaviour, Iinteractable
    {
        [SerializeField] List<string> _dialogueQuest = new List<string>();
        public bool _canInteract = true;
        public bool _isQuestOnOpen = true;
        public int _questId = 0;
        public bool _interactOnTrigger;
        private bool _isFirstTime = true;
        private int _currentDialogue = 0;

        [SerializeField] private GameObject _kertasToShow;

        private bool _isOnZone = false;

        private ThirdPersonController thirdPersonController;

        private void Start()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            if (GameManager.Instance.DialogueManager == null)
            {
                GameManager.Instance.InitializeComponents();
            }
        }

        public void Interact()
        {
            if (!_canInteract)
                return;

            StartInteract();
        }

        void StartInteract()
        {
            if (!_isOnZone)
                return;

            if (thirdPersonController == null)
            {
                thirdPersonController = FindObjectOfType<ThirdPersonController>();
                if (thirdPersonController == null)
                {
                    Debug.LogError("ThirdPersonController is not assigned.");
                    return;
                }
            }

            thirdPersonController.DisablePlayer();

            if (_isQuestOnOpen)
            {
                _isQuestOnOpen = false;
                GameManager.Instance.questManager.ChangeQuest(_questId);
            }

            if (_isFirstTime)
            {
                StartDialogue();
                GameManager.Instance.questManager.HideQuestPanel();
                return;
            }

            ToggleKertas();
        }

      public  void ToggleKertas()
        {
            if (_kertasToShow == null)
            {   
                thirdPersonController.EnablePlayer();
                return;
            }
            _kertasToShow.SetActive(!_kertasToShow.activeSelf);

            if (_kertasToShow.activeSelf)
            {
                thirdPersonController.DisablePlayer();
            }
            else
            {
                thirdPersonController.EnablePlayer();
            }
        }

        void StartDialogue()
        {
            if (_currentDialogue == _dialogueQuest.Count)
            {
                _isFirstTime = false;

                // Null check for DialogueManager
                if (GameManager.Instance.DialogueManager != null)
                {
                    GameManager.Instance.DialogueManager.HideDialogue();
                }
                else
                {
                    Debug.LogError("DialogueManager is not assigned in GameManager.");
                }

                ToggleKertas();
            }
            else
            {
                // Null check for DialogueManager
                if (GameManager.Instance.DialogueManager != null)
                {
                    GameManager.Instance.DialogueManager.StartDialogue(_dialogueQuest[_currentDialogue], false);
                    _currentDialogue++;
                }
                else
                {
                    Debug.LogError("DialogueManager is not assigned in GameManager.");
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (thirdPersonController == null)
                {
                    thirdPersonController = other.GetComponent<ThirdPersonController>();
                }

                _isOnZone = true;

                if (_interactOnTrigger)
                {
                    Interact();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isOnZone = false;
                GameManager.Instance.questManager.ShowQuestPanel();

            }
        }
    }
}
