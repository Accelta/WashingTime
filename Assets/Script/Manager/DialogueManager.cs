
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueTextMesh; // Fixed the typo from dialogueTextMash to dialogueTextMesh
    [SerializeField] private string dialogueToDisplay;
    [SerializeField] private float typingSpeed = 0.05f; // Speed of typing effect
    [SerializeField] private GameObject _dialoguePanel;

    [SerializeField] private bool InteractAbility = false;
    Coroutine _dialogueCoroutine;

    // Event to notify when the dialogue is finished
    public event System.Action OnDialogueComplete;

    private void Start()
    {
            
    }

    public void StartDialogue(string text, bool isDisable)
    {
        dialogueToDisplay = text;

        if (_dialogueCoroutine != null)
        {
            StopCoroutine(_dialogueCoroutine);
            _dialogueCoroutine = null;
        }

        _dialogueCoroutine = StartCoroutine(TypeDialogue(isDisable));
    }

    private IEnumerator TypeDialogue(bool isDisable)
    {
        _dialoguePanel.SetActive(true);
        dialogueTextMesh.text = ""; // Clear the text before starting the effect
        foreach (char letter in dialogueToDisplay.ToCharArray())
        {
            dialogueTextMesh.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(3);

        if (isDisable)
        {
            _dialoguePanel.SetActive(false);
        }

        // Trigger the event when the dialogue is complete
        OnDialogueComplete?.Invoke();
    }

    public void HideDialogue()
    {
        if (_dialogueCoroutine != null)
        {
            StopCoroutine(_dialogueCoroutine);
        }

        _dialoguePanel.SetActive(false);
    }
}
