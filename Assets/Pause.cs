using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public GameObject canvas;


    private void Start()
    {
        canvas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //turn on pause
            if(canvas.gameObject.activeSelf == false)
            {
                PauseGame();
            }


            //turn off pause
            else if (canvas.gameObject.activeSelf == true)
            {
                Resume();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0.0f;
        print("pause");
        canvas.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        canvas.gameObject.SetActive(false);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        
    }

    public void ExitToTitle()
    {
        SceneManager.LoadScene("Title");

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
