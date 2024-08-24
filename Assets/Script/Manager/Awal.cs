// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Awal : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
//         GameManager.Instance.DialogueManager.StartDialogue("Ternyata sudah pagi, Dimana ya ayah dan ibu?", true);
//         GameManager.Instance.questManager.HideQuestPanel();

//         // Subscribe to the dialogue complete event
//         GameManager.Instance.DialogueManager.OnDialogueComplete += ShowQuestPanel;
//     }

//     // Show the quest panel when the dialogue is complete
//     private void ShowQuestPanel()
//     {
//         GameManager.Instance.questManager.ShowQuestPanel();
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     private void OnDestroy()
//     {
//         // Unsubscribe from the event when this object is destroyed to avoid memory leaks
//         if (GameManager.Instance != null && GameManager.Instance.DialogueManager != null)
//         {
//             GameManager.Instance.DialogueManager.OnDialogueComplete -= ShowQuestPanel;
//         }
//     }
// }
