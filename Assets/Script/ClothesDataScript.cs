using UnityEngine;

[CreateAssetMenu(fileName = "New Clothes", menuName = "Clothes Data")]
public class ClothesDataScript : ScriptableObject
{
    public WashingStep[] washingSteps;
    public Sprite[] clothingSprites;
}

[System.Serializable]
public class WashingStep
{
    public string action;
}
