using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : GameBehaviour<UIManager>
{
    public TMP_Text scoreText;
    int score = 0;
    int scoreBouns = 50;
    public Ease scoreEase;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TweenScore()
    {
        DOTween.To(() => score, x => score = x, score + scoreBouns, 1).OnUpdate(() =>
        {
            scoreText.text = "Score: " + score;
        }).SetEase(scoreEase);
    }
}
