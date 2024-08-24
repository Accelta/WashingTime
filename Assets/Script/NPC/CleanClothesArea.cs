using UnityEngine;

public class CleanClothesArea : MonoBehaviour
{
    public int cleanClothesCount = 0;

    public void AddCleanClothes(int count)
    {
        cleanClothesCount += count;
        Debug.Log("Clean clothes added. Current count: " + cleanClothesCount);
    }

    public bool TakeCleanClothes()
    {
        if (cleanClothesCount > 0)
        {
            cleanClothesCount--;
            Debug.Log("Clean clothes taken. Remaining count: " + cleanClothesCount);
            return true;
        }
        else
        {
            Debug.Log("No clean clothes available!");
            return false;
        }
    }
}
