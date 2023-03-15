using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using universal;

public class Instructions : GameBehaviour
{
    public GameObject instructionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseInstructions()
    {
        _GAMESTATE.ChangeGameState(Gamestate.Playing);

        instructionsPanel.SetActive(false);
    }
}
