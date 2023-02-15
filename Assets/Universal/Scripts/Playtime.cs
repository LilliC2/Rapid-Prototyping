using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playtime : GameBehaviour
{

    public GameObject player; //temp

    // Start is called before the first frame update
    void Start()
    {
        ExecuteAfterSeconds(2, ()=> ScaleToZero(player));

        //How to do multiple things after x 
        ExecuteAfterFrames(2, () =>
        {
            ChangePlayerColour();
            print("wow");
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangePlayerColour();
        }
    }

    void ChangePlayerColour()
    {
        player.GetComponent<Renderer>().material.color = GetRandomColour();
    }
}
