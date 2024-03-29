using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseController : GameBehaviour
{
    public GameObject pausePanel;
    bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1; //means we are running at real time, setting it at 2 means time runs as twice as fast etc. setting timescale to 0 is a pause
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (isPaused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

   public void Pause()
    {
        _GM4.gameStates = prototype4.GameManager.GameStates.Pause;

        Cursor.lockState = isPaused ? CursorLockMode.Locked : CursorLockMode.None;
        isPaused = !isPaused; //flip switch
        print("Paused: " + isPaused);
        pausePanel.SetActive(isPaused);
        
        //Time.timeScale = isPaused ? 0 : 1; //if isPaused is true, timeScale 0 else 1
    }



    public void Exit()
    {
        Application.Quit();
    }
}
