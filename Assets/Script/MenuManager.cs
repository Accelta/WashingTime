using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject selectLevelPanel;

    private void Start()
    {
        // Check the flag from WinManager to determine which panel to show
        if (WinManager.returnToSelectLevel)
        {
            ShowSelectLevelPanel();
            WinManager.returnToSelectLevel = false; // Reset the flag after use
        }
        else
        {
            ShowMenuPanel();
        }
    }

    public void ShowMenuPanel() {
        menuPanel.SetActive(true);
        selectLevelPanel.SetActive(false);
    }

    public void ShowSelectLevelPanel() {
        menuPanel.SetActive(false);
        selectLevelPanel.SetActive(true);
    }
}
