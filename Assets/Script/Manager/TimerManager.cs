// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class Timer : MonoBehaviour
// {
//     [SerializeField] TextMeshProUGUI timerText;
//     [SerializeField] float remaningTime;

//     private void Update()
//     {
//         if (remaningTime > 0)
//         {
//             remaningTime -= Time.deltaTime;
//         }else if (remaningTime < 0)
//         {
//             remaningTime = 0;
//             GameOver();
//         }

//         int minutes = Mathf.FloorToInt(remaningTime / 60);
//         int second = Mathf.FloorToInt(remaningTime % 60);

//         timerText.text = string.Format ("{00 : 00} : {1: 00}", minutes, second);

//     }

//     private void GameOver()
//     {
//         Debug.Log("GameOver");
//         GameManager.Instance.TriggerGameOver();
//     }
// }