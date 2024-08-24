using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;
    private bool timerStarted = false;

    private void Start()
    {
        // Find the TeksAwal object and subscribe to its OnDialogueComplete event
        TeksAwal teksAwal = FindObjectOfType<TeksAwal>();
        if (teksAwal != null)
        {
            teksAwal.OnDialogueComplete += StartTimer;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        TeksAwal teksAwal = FindObjectOfType<TeksAwal>();
        if (teksAwal != null)
        {
            teksAwal.OnDialogueComplete -= StartTimer;
        }
    }

    private void StartTimer()
    {
        timerStarted = true;
    }

    private void Update()
    {
        if (timerStarted)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime <= 0)
            {
                remainingTime = 0;
                GameOver();
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        GameOverManager.Instance.TriggerGameOver();
    }
    public float GetRemainingTime()
{
    return remainingTime;
}
public void stoptimer(){
    timerStarted = false;
    Time.timeScale = 0f;
}
public void playtimer(){
     timerStarted = true;
    Time.timeScale = 1f;
}
}
