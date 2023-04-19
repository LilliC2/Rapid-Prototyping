using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : GameBehaviour
{

    public int score;
    public int highscore;
    public string playTime;
    GameDataManager _DATA;

    // Start is called before the first frame update
    void Start()
    {
        _DATA = GameDataManager.INSTANCE;
        highscore = _DATA.GetHighestScore();
        playTime = _DATA.GetTimeFormatted();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.Space)) score++;
    }

    void Save()
    {
        _DATA.SetScore(score);
        _DATA.SetTimePlayed();
        _DATA.SaveData();
    }
}
