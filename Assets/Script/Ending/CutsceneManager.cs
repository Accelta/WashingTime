using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer video;
    public Button skipbutton;
    public string SceneName;

    void Start()
    {
        skipbutton.onClick.AddListener(SkipCutscene);
        video.loopPointReached += OnVideoEnd;
        video.Play();
    }

    public void SkipCutscene()
    {
        // Load the main game scene
        LoadScene(SceneName);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadScene(SceneName);
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
