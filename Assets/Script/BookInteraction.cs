using System.Collections;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookInteraction : MonoBehaviour
{
    public GameObject dialogueBox;
    public Image Chara;
    public Sprite[] Charasprite;
    public TextMeshProUGUI dialogueText;
    public AudioSource PaperSound;
    public string[] dialogues;
    private int dialogueIndex = 0;
    private bool inRange = false;
    private bool hasInteracted = false; // Flag to track if player has interacted with the book

    // Typewriter effect variables
    private string currentDialogue = "";
    private float typingSpeed = 0.05f; // Adjust the typing speed as needed
    private bool isTyping = false;
    public GameObject sheetImage; // Reference to the UI Image component
    private ThirdPersonController characterController;

    private Animator _playerAnim;

    void Start()
    {
        dialogueBox.SetActive(false);
        sheetImage.SetActive(false); // Ensure the sheet image is initially hidden
        characterController = FindObjectOfType<ThirdPersonController>();
        _playerAnim = characterController.GetComponent<Animator>();
    }

    void Update()
    {
        if (inRange && Input.GetMouseButtonDown(0))
        {
            if (!isTyping && dialogueIndex < dialogues.Length)
            {
                // If not currently typing and there are more dialogues, start typing the next dialogue
                StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
            }
            else if (isTyping)
            {
                // If currently typing, instantly display the entire dialogue
                StopAllCoroutines();
                dialogueText.text = dialogues[dialogueIndex];
                isTyping = false;
                dialogueIndex++;
            }
            else if (!isTyping && dialogueIndex >= dialogues.Length && !hasInteracted)
            {
                // If there are no more dialogues to display and first interaction, show the sheet image
                Debug.Log("No more dialogues. Showing the sheet image.");
                hasInteracted = true;
                StartCoroutine(WaitForClickToShowSheet());
            }
            else if (!isTyping && hasInteracted)
            {
                // If already interacted once, directly show the sheet image
                Debug.Log("Already interacted. Showing the sheet image.");
                sheetImage.SetActive(true);

            }
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;
        dialogueText.text = "";
        currentDialogue = dialogue;

  if (Charasprite.Length > dialogueIndex && Charasprite[dialogueIndex] != null)
        {
            Chara.sprite = Charasprite[dialogueIndex];
        }

        // Typewriter effect loop
        foreach (char letter in dialogue.ToCharArray())
        {
            if (characterController != null)
            {
                characterController.enabled = false;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Typing finished
        isTyping = false;
        dialogueIndex++;

        // Check if there are more dialogues, if not, set hasInteracted to true
        if (dialogueIndex >= dialogues.Length)
        {
            if (characterController != null)
            {
                characterController.enabled = true;
            }
        }
    }

    void ShowSheetImage()
    {
        PaperSound.Play();
        sheetImage.SetActive(true);
        dialogueBox.SetActive(false);
        if (characterController != null)
        {
            characterController.enabled = true;
        }
        Debug.Log("Sheet image should now be visible.");
    }

    IEnumerator WaitForClickToShowSheet()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        ShowSheetImage();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            if (!hasInteracted)
            {
               

                //StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                dialogueBox.SetActive(true);
                dialogueIndex = 0; // Reset dialogue index when player enters range
            }
            else
            {
                sheetImage.SetActive(true); // Show sheet image if already interacted
                dialogueBox.SetActive(false);
            }

            GameManager.Instance.questManager.HideQuestPanel();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            StopAllCoroutines(); // Stop any ongoing typewriter effect
            dialogueBox.SetActive(false);
            sheetImage.gameObject.SetActive(false); // Hide sheet image when player exits range
            if (!hasInteracted)
            {
                dialogueIndex = 0; // Reset dialogue index only if player hasn't completed interaction
            }

            GameManager.Instance.questManager.ShowQuestPanel();

        }
    }
}

