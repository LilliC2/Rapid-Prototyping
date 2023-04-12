using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace prototype4
{
    public class GameManager : GameBehaviour<GameManager>
    {
        bool answerGen;
        public bool solved;

        int answersSolved;

        public GameObject losePanel;
        public GameObject inGanePanel;
        public TMP_Text answeredSolved;
        public TMP_Text questionText;

        public enum GameStates { Pause, Spawning, Solving, Lose }

        public GameStates gameStates;
        // Start is called before the first frame update
        void Start()
        {

            gameStates = GameStates.Spawning;
        }

        // Update is called once per frame
        void Update()
        {
            switch (gameStates)
            {

                case GameStates.Spawning:
                    solved = false;
                    DifficultyCheck();
                    if (!answerGen)
                    {
                        _MS.DestroyFood();
                        _EG.GenerateRandomEquation();
                        questionText.text = _EG.question;
                        _EG.GenerateDummyAnswers();
                        answerGen = true;

                    }


                    if (_MS.FoodGenerated.Count < 4)
                    {
                        print("SPAWNING");
                        ExecuteAfterFrames(20, () => _MS.SpawnFood()); 
                    }

                    gameStates = GameStates.Solving;

                    break;
                case GameStates.Solving:
                    if (solved)
                    {
                        answersSolved++;
                        _PC4.GrowColony();
                        
                        answerGen = false;
                        gameStates = GameStates.Spawning;
                        
                        
                    }
                    break;
                case GameStates.Lose:

                    inGanePanel.SetActive(false);
                    losePanel.SetActive(true);
                    answeredSolved.text = "Answered solved: " + answeredSolved;

                    break;
            }

            

        }

        void DifficultyCheck()
        {
            if (_PC4.AntLine.Count > 10) _EG.difficulty = EquationGenerator.Difficulty.EASY;
            if (_PC4.AntLine.Count > 10 && _PC4.AntLine.Count < 20) _EG.difficulty = EquationGenerator.Difficulty.MEDIUM;
            if (_PC4.AntLine.Count > 20 && _PC4.AntLine.Count < 30) _EG.difficulty = EquationGenerator.Difficulty.HARD;
        }
    }

    }




