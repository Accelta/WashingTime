using Script.Interface;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class ShowWashUI : MonoBehaviour, Iinteractable
{
    private bool isinteract = false;
    public GameObject washUI;
    public Collider waterBucketCollider;
    public Button exitButton; // Reference to the Exit button in the UI

    private bool isPlayerInside = false;
    public ThirdPersonController characterController; // Reference to the CharacterController
    private bool isWash = false;
    public bool _canInteract = false;
    [SerializeField] private GameObject interactionClueUI;



    private void Start()
    {
        washUI.SetActive(false);
        characterController = FindObjectOfType<ThirdPersonController>(); // Get the ThirdPersonController component from the scene

        // Add listener to the Exit button
        exitButton.onClick.AddListener(OnExitButtonClicked);
        interactionClueUI.SetActive(false);

    }

    // private void Update()
    // {
    //     // Check if the player clicks left mouse button when inside the water bucket collider
    //     if (Input.GetMouseButtonDown(0) && isPlayerInside)
    //     {
    //         // Check if all clothes have been collected
    //         if (Inventory.instance.IsInventoryFull())
    //         {
    //             ShowUI();
    //         }
    //     }


    // }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.questManager.HideQuestPanel();
        if (!isinteract)
        {

            if (other.CompareTag("Player"))
            {
                isPlayerInside = true;
                interactionClueUI.SetActive(true);
                GameManager.Instance.DialogueManager.StartDialogue("Kita harus merendam pakaiannya 30 menit sebelum dicuci", true);
                isinteract = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            HideWashUI();
            interactionClueUI.SetActive(false);
        GameManager.Instance.questManager.ShowQuestPanel();

        }
    }

    private void ShowUI()
    {
        interactionClueUI.SetActive(false);
        GameManager.Instance.questManager.HideQuestPanel();
        washUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // Disable player movement
        if (characterController != null)
        {
            characterController.gameObject.SetActive(false);
        }

        WashManager showWashUI = washUI.GetComponent<WashManager>();
        showWashUI.ShowWashUi = this;

        //isWash = true;
        isPlayerInside = false;
    }

    public void HideWashUI()
    {
        washUI.SetActive(false);
        if (characterController != null)
        {
            characterController.gameObject.SetActive(true);
        }
    }

    private void OnExitButtonClicked()
    {
        HideWashUI();
    }

    public void Interact()
    {
        Debug.LogError("Interact");
        if (_canInteract)
        {
            ShowUI();

        }
    }

    public void DisableInteraction()
    {
        _canInteract = false;
        interactionClueUI.SetActive(false);
        isinteract = true;
    }
}
