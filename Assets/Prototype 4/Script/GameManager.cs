using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace prototype4
{
    public class GameManager : GameBehaviour
    {
        bool solved = true;
        bool spawned;


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (spawned == false)
            {
                spawned = true;
 
                _EG.GenerateRandomEquation();
                SpawnFoodAnswers();
                
            }
        }

        void SpawnFoodAnswers()
        {
            _EG.GenerateDummyAnswers();
            _MS.SpawnFood();

        }
    }


}

