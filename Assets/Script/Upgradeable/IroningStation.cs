using UnityEngine;

public class IroningStation : MonoBehaviour
{
    public int clothesToIron = 0;

    public void AddClothes(int count)
    {
        clothesToIron += count;
        Debug.Log("Added " + count + " clothes to be ironed.");
    }

    public bool IronClothes()
    {
        if (clothesToIron > 0)
        {
            clothesToIron--;
            Debug.Log("Ironed one piece of clothing. Remaining: " + clothesToIron);
            return true;
        }
        else
        {
            Debug.Log("No clothes to iron!");
            return false;
        }
    }

    public void FinishIroning(CleanClothesArea cleanClothesArea)
    {
        cleanClothesArea.AddCleanClothes(1);
        Debug.Log("Added one ironed piece of clothing to the clean clothes area.");
    }
}
