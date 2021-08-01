using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Retry ()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
        KeyItem.keyCount = 0;
    }
    
    public void BackMenu ()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        KeyItem.keyCount = 0;
    }

    public void QuitGame ()
    {
        Time.timeScale = 1f;
        KeyItem.keyCount = 0;
        Application.Quit();
    }
}
