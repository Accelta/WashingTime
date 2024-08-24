using System;
using System.Collections;
using System.Collections.Generic;
using Script.Jemuran;
using Script.SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class WashManager : MonoBehaviour
{
    public BajuManagerSO BajuManagerSo;
    [SerializeField] private BajuDragAble _bajuDragAble;
    public ShowWashUI ShowWashUi;
    public JemuranManager JemuranManager;
    private int _currectBaju = 0; //baju saat ini
    private int _delayanimayion = 1;
    public int lvlid;
    [SerializeField] private WinManager winmanager;

    private void OnEnable()
    {

        InitWashManager();
        GameManager.Instance.questManager.HideQuestPanel();
    }

    void InitWashManager()
    {
        _bajuDragAble.InitBaju(BajuManagerSo.BajuSoList[_currectBaju]);

    }

    public void NextBaju()
    {
        _currectBaju++;
        if (_currectBaju >= BajuManagerSo.BajuSoList.Count)
        {
            _currectBaju = 0;
            StartCoroutine(WaitForAnimation());
            return;
        }
        _bajuDragAble.InitBaju(BajuManagerSo.BajuSoList[_currectBaju]);
    }

    private IEnumerator WaitForAnimation()
    {
        if (lvlid == 2)
        {
            yield return new WaitForSeconds(_delayanimayion);
            ShowWashUi.characterController.gameObject.SetActive(true);
            gameObject.SetActive(false);
            if (winmanager != null)
            {
                winmanager.ShowScore();
            }
            ShowWashUi.DisableInteraction();
        }
        else
        {
            yield return new WaitForSeconds(_delayanimayion);
            ShowWashUi.characterController.gameObject.SetActive(true);
            GameManager.Instance.questManager.ChangeQuest(4);
            gameObject.SetActive(false);
            ShowWashUi.DisableInteraction();
        }
    }

    public void kembali()
    {
        gameObject.SetActive(false);
        ShowWashUi.characterController.gameObject.SetActive(true);

    }
    private void OnDisable()
    {
        GameManager.Instance.questManager.ShowQuestPanel();
    }
}
