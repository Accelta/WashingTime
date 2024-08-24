using UnityEngine;

public class ClothesManager : MonoBehaviour
{
    public static ClothesManager Instance;

    public ClothesStep[] clothesArray;
    private int currentClothesIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ShowNextClothes();
    }

    public void ShowNextClothes()
    {
        if (currentClothesIndex < clothesArray.Length)
        {
            for (int i = 0; i < clothesArray.Length; i++)
            {
                clothesArray[i].gameObject.SetActive(i == currentClothesIndex);
            }
            currentClothesIndex++;
        }
        else
        {
            Debug.Log("All clothes have been washed.");
        }
    }
}
