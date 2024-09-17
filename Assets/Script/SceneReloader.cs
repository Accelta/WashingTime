using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    // Call this method when you want to reset the scene
    public void ResetScene()
    {
        #if UNITY_EDITOR
        // In the Editor, stop play mode when Reset is triggered
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #else
        // In Build mode, reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        #endif
    }

    // Optional: Reset scene when the player presses 'R'
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }
}
