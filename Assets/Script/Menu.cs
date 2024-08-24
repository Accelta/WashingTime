using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // [SerializeField] GameObject UiLevel;

    public void lvl1()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);

        }
        public void quit ()
        {
            Application.Quit();
            Debug.Log("Berhasil Keluar");
        }

     public void lvl2()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2);

        }
         public void lvl3()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +3);

        }
        
}
