using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : GameBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(0, player.transform.position.y+2, -13.8f);

    }
}
