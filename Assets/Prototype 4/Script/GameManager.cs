using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace prototype4
{
    public class GameManager : GameBehaviour<GameManager>
    {
        bool answerGen;
        public bool solved;

        public enum GameStates { Pause, Spawning, Solving }

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
                    if (!answerGen)
                    {
                        _MS.DestroyFood();
                        _EG.GenerateRandomEquation();
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
                        _PC4.GrowColony();
                        
                        answerGen = false;
                        gameStates = GameStates.Spawning;
                        
                        
                    }
                    break;
            }

            

        }

    }

    }




