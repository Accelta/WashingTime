using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseui;
    [SerializeField] private RectTransform pauserect;
    [SerializeField] private CanvasGroup Canvasgroup;
    private bool ispause = false;
    [SerializeField] private float timer = 1;
    [SerializeField] private float posYatas, posYmasuk;
    public string sceneName;
    [SerializeField] private Timer time;
    public static bool returnToSelectLevel = false;

    private void Start()
    {
        pauseui.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!ispause)
            {
                pause();
            }
            else
            {
                resume();
            }
        }
    }

    public void pause()
    {
        ispause = true;
        pauseui.SetActive(true);
        Time.timeScale = 0;
        intro();
    }

    public async void resume()
    {
        ispause = false;
        await outro();
        pauseui.SetActive(false);
        Time.timeScale = 1;
    }

    void intro()
    {
        Canvasgroup.DOFade(1, timer).SetUpdate(true);
        pauserect.DOAnchorPosY(posYmasuk, timer).SetUpdate(true);
    }

    async Task outro()
    {
        Canvasgroup.DOFade(0, timer).SetUpdate(true);
        await pauserect.DOAnchorPosY(posYatas, timer).SetUpdate(true).AsyncWaitForCompletion();
    }

    public void LoadScene(string sceneName)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGameState();
        }

        // Indicate that we should show the Select Level panel when we return to the menu
        returnToSelectLevel = true;

        SceneManager.LoadScene(sceneName);

        // Ensure GameManager components are reinitialized
        if (GameManager.Instance != null)
        {
            GameManager.Instance.InitializeComponents();
        }

        time.playtimer();
    }
}

