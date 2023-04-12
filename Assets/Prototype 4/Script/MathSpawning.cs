using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MathSpawning : GameBehaviour<MathSpawning>
{

    public GameObject foodPrefab;
    private List<GameObject> FoodGenerated = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyUp(KeyCode.K)) SpawnFood();

    }

    public void SpawnFood()
    {
        //generate 4 food

        for (int i = 0; i < 4; i++)
        {
            Vector3 pos = RandomSpawnPoint();
            GameObject food = Instantiate(foodPrefab, pos, Quaternion.identity);
            FoodGenerated.Add(food);
            TextMeshPro answerText = food.GetComponentInChildren<TextMeshPro>(true);

            if (i==0) //make correct answer
            {
                answerText.text = _EG.correctAnswer.ToString();
                food.GetComponent<Renderer>().material.color = Color.green;
            }
            else answerText.text = _EG.dummyAnswers[i].ToString();


        }

        //give food dummy answers 

        //if correct food is eaten, delete all food
    }

    Vector3 RandomSpawnPoint()
    {
        Vector3 pos = new Vector3(RandomFloatBetweenTwoFloats(-24, 24), 1f, RandomFloatBetweenTwoFloats(-24, 24));
        return pos;
    }
}
