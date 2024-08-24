using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class TeksAwal : MonoBehaviour
{
    private bool isdone = false;
    [SerializeField] private List<string> teksawal = new List<string>();
    private int _currentDialogue = 0;

    private ThirdPersonController thirdPersonController;

    // Event to notify when the dialogue is complete
    public event System.Action OnDialogueComplete;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player and its ThirdPersonController component
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            thirdPersonController = player.GetComponent<ThirdPersonController>();
            if (thirdPersonController != null)
            {
                thirdPersonController.DisablePlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isdone)
        {
            StartDialogue();
            isdone = true;
            GameManager.Instance.questManager.HideQuestPanel();
            GameManager.Instance.DialogueManager.OnDialogueComplete += HandleDialogueComplete;
        }
    }

    private void StartDialogue()
    {
        if (_currentDialogue < teksawal.Count)
        {
            GameManager.Instance.DialogueManager.StartDialogue(teksawal[_currentDialogue], false);
        }
        else
        {
            HandleDialogueComplete();
        }
    }

    private void HandleDialogueComplete()
    {
        _currentDialogue++;
        if (_currentDialogue < teksawal.Count)
        {
            StartDialogue();
        }
        else
        {
            if (thirdPersonController != null)
            {
                GameManager.Instance.questManager.ShowQuestPanel();
                thirdPersonController.EnablePlayer();
            }
            GameManager.Instance.DialogueManager.HideDialogue();
            GameManager.Instance.DialogueManager.OnDialogueComplete -= HandleDialogueComplete;

            // Trigger the event to notify that the dialogue is complete
            OnDialogueComplete?.Invoke();
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null && GameManager.Instance.DialogueManager != null)
        {
            GameManager.Instance.DialogueManager.OnDialogueComplete -= HandleDialogueComplete;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optionally, handle logic when the player exits the trigger
    }
}
