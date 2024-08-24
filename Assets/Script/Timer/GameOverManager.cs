using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private static GameOverManager _instance;

    public static GameOverManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Find the GameOverManager in the scene
                _instance = FindObjectOfType<GameOverManager>();

                if (_instance == null)
                {
                    // If there is no GameOverManager in the scene, create one
                    GameObject singleton = new GameObject("GameOverManager");
                    _instance = singleton.AddComponent<GameOverManager>();

                    // Ensure the GameOverManager persists across scenes
                    DontDestroyOnLoad(singleton);
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        // Check if there is already an instance of GameOverManager
        if (_instance == null)
        {
            // Set the instance to this GameOverManager
            _instance = this;
            // Make sure this GameOverManager persists across scenes
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            // If there is another instance, destroy this one
            Destroy(gameObject);
        }
    }

    public void TriggerGameOver()
    {
        // Implement your game over logic here
        // For example, load the game over scene
        SceneManager.LoadScene("BadEndingScene");
    }
}
