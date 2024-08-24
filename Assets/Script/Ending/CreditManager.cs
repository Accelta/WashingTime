using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CreditManager : MonoBehaviour
{
    public VideoPlayer video;
    public Button skipButton;
    public RawImage rawImage;
    public string sceneName;

    void Awake()
    {
        skipButton.onClick.AddListener(SkipCutscene);
        video.loopPointReached += OnVideoEnd;
    }

    void Start()
    {
        video.Play();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect when the left mouse button is pressed
        {
            SpeedUpVideo();
        }

        if (Input.GetMouseButtonUp(0)) // Detect when the left mouse button is released
        {
            ResetSpeedVideo();
        }

        // Ensure rawImage updates every frame
        if (video.isPlaying)
        {
            rawImage.texture = video.texture;
        }
    }

    public void SkipCutscene()
    {
        LoadScene(sceneName);
    }

    void SpeedUpVideo()
    {
        video.playbackSpeed = 2.0f; // Speed up video playback
    }

    void ResetSpeedVideo()
    {
        video.playbackSpeed = 1.0f; // Reset video playback speed to normal
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGameState();
        }

        SceneManager.LoadScene(sceneName);
        
        // Ensure GameManager components are reinitialized
        if (GameManager.Instance != null)
        {
            GameManager.Instance.InitializeComponents();
        }
    }
}
