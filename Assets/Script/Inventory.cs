using System.Collections.Generic;

using Script.SO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Image _currectBajuDisplay;
    public Color hilang;
    public Color muncul;
    public BajuSo CurrectBajuSo;
    public int MaxsimalBaju = 10;
    public int _currectBaju = 0;
    public int maxClothes = 5;
    private List<GameObject> clothesList = new List<GameObject>();
    [SerializeField] private WinManager winmanager;

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

    public bool AddClothes(GameObject clothes)
    {
        if (clothesList.Count < maxClothes)
        {
            clothesList.Add(clothes);
            clothes.SetActive(false);
            return true;
        }
        else
        {
            Debug.Log("Inventory full");
            return false;
        }
    }

    public void RemoveClothes(GameObject clothes)
    {
        clothesList.Remove(clothes);
    }

    public bool IsInventoryFull()
    {
        return clothesList.Count >= maxClothes;
    }

    public void SetCurretBaju(BajuSo bajuSo)
    {
        _currectBajuDisplay.sprite = bajuSo.DisplayBajuInventory;
        _currectBajuDisplay.color = muncul;
        CurrectBajuSo = bajuSo;
    }

    public void ResetBaju()
    {
        _currectBajuDisplay.color = hilang;
        CurrectBajuSo = null;
        _currectBaju++;

        if (_currectBaju == MaxsimalBaju)
        {
            if (winmanager != null)
            {
                winmanager.ShowScore();
            }
            else
            {
                Debug.LogWarning("WinManager is not assigned.");
            }
        }
    }

    public void InitializeInventory()
    {
        _currectBajuDisplay.color = hilang;
        CurrectBajuSo = null;
        _currectBaju = 0;
        clothesList.Clear();
    }
}

