using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MathSpawning : GameBehaviour<MathSpawning>
{

    public GameObject foodPrefab;
    public List<GameObject> FoodGenerated = new List<GameObject>();


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


            //give food dummy answers 

            TMP_Text answerText = food.GetComponentInChildren<TMP_Text>(true);

            if (i==0) //make correct answer
            {
                answerText.text = _EG.correctAnswer.ToString();
                food.GetComponent<Renderer>().material.color = Color.green;
            }
            else answerText.text = _EG.dummyAnswers[i].ToString();


        }

        
    }

    Vector3 RandomSpawnPoint()
    {
        Vector3 pos = new Vector3(RandomFloatBetweenTwoFloats(-20, 20), 1f, RandomFloatBetweenTwoFloats(-20, 20));
        return pos;
    }

    public void DestroyFood()
    {
        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
        foreach(GameObject foodToDestroy in food)
            GameObject.Destroy(foodToDestroy);
        FoodGenerated.Clear();
        
    }

    public void DestroyWrongAnswer(GameObject _food)
    {
        int index = FoodGenerated.IndexOf(_food);
        FoodGenerated.RemoveAt(index);
        Destroy(_food);
    }


}
