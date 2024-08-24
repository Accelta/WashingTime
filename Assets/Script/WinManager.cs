using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public static WinManager instance;
    public GameObject perfectUI;
    public GameObject goodUI;
    public GameObject NotgoodUI;
    public RectTransform perfectUIRect;
    public RectTransform goodUIRect;
    public RectTransform NotgoodUIRect;
    public CanvasGroup perfectUICanvasGroup;
    public CanvasGroup goodUICanvasGroup;
    public CanvasGroup NotgoodUICanvasGroup;
    public float animationDuration = 1.0f;
    public float targetYPositionAwal, targetYPositionAkhir;
    public Timer time;
    public string sceneName;
    [SerializeField] private float perfectTime;
    [SerializeField] private float goodTime;
    public static bool returnToSelectLevel = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowScore()
    {
        if (time != null)
        {
            float remainingTime = time.GetRemainingTime();
            if (remainingTime >= perfectTime)
            {
                ShowUI(perfectUI, perfectUIRect, perfectUICanvasGroup);
            }
            else if (remainingTime >= goodTime)
            {
                ShowUI(goodUI, goodUIRect, goodUICanvasGroup);
            }
            else
            {
                ShowUI(NotgoodUI, NotgoodUIRect, NotgoodUICanvasGroup);
            }
            time.stoptimer();
        }
    }

    private void ShowUI(GameObject uiElement, RectTransform uiRect, CanvasGroup uiCanvasGroup)
    {
        uiElement.SetActive(true);
        uiCanvasGroup.alpha = 0;
        uiRect.anchoredPosition = new Vector2(uiRect.anchoredPosition.x, targetYPositionAwal);
        uiCanvasGroup.DOFade(1, animationDuration).SetUpdate(true);
        uiRect.DOAnchorPosY(targetYPositionAkhir, animationDuration).SetUpdate(true);
    }

    private async Task HideUI(RectTransform uiRect, CanvasGroup uiCanvasGroup)
    {
        await uiCanvasGroup.DOFade(0, animationDuration).SetUpdate(true).AsyncWaitForCompletion();
        await uiRect.DOAnchorPosY(targetYPositionAwal, animationDuration).SetUpdate(true).AsyncWaitForCompletion();
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
