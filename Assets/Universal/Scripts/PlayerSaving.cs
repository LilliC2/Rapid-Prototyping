using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaving : GameBehaviour
{
    int score = 0;
    int highscore = 0;

    private void Start()
    {
        print("Score: " + score);
        highscore = PlayerPrefs.GetInt("highscore");
        print("HighScore: " + highscore);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        { score++;
            print("Score: " + score);
        }

        if (Input.GetKeyDown(KeyCode.G)) GameOver();
        if (Input.GetKeyDown(KeyCode.P)) PlayerPrefs.DeleteAll(); //or DeleteKey("highscore");

    }

    void GameOver()
    {
        print("Game over");

        //string is the key to find it
        if(score>highscore)
        {
            highscore = score;
            print("New highscore: " + highscore);
            PlayerPrefs.SetInt("highscore", score);
        }
        
    }


}
