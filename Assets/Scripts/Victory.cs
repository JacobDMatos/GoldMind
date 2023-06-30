using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public GameObject victoryText;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        victoryText.SetActive(false);
        timer = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
       if (!GameObject.FindGameObjectWithTag("Boss"))
        {
            victoryText.SetActive(true);
            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                SceneManager.LoadScene(0);
            }
        } 
    }
}
