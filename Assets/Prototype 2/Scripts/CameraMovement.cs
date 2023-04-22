using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace prototype2
{
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
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+10, player.transform.position.z- 9);
        }
    }
}


