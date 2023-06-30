using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public int sceneToLoad = 1;
    public void Start()
    {
        Time.timeScale = 1.0f;
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
