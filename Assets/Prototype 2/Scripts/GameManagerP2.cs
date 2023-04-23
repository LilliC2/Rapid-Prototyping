using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace prototype2
{
    public class GameManagerP2 : GameBehaviour<GameManagerP2>
    {

        public enum GameState {  Playing, Gameover}
        public GameState gameState;
        public GameObject gameOverPanel;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            switch(gameState)
            {
                case GameState.Playing:
                    Time.timeScale = 1;
                    break;
                case GameState.Gameover:

                    Time.timeScale = 0;
                    gameOverPanel.SetActive(true);
                    break;
            }

        }
    }
}


