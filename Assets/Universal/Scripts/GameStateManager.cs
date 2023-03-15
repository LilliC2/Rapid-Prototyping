using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameBehaviour;

public class GameStateManager : GameBehaviour<GameStateManager>
{
    public Gamestate gamestate;
    public void ChangeGameState(Gamestate _gamestate)
    {
        gamestate = _gamestate;
    }
}
