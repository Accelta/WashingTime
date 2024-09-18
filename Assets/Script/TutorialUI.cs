using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public Image tutorialImage; // Reference to the Image component

    // Start is called before the first frame update
    void Start()
    {
        if (tutorialImage != null)
        {
            tutorialImage.gameObject.SetActive(false); // Ensure the image is initially inactive
        }
    }

    public void ShowTutorial()
    {
        if (tutorialImage != null)
        {
            tutorialImage.gameObject.SetActive(true); // Activate the image
        }
    }

    public void Close()
    {
        if (tutorialImage != null)
        {
            tutorialImage.gameObject.SetActive(false); // Deactivate the image
        }
    }
}
