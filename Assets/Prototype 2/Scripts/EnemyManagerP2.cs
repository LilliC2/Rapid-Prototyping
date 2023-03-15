using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerP2 : GameBehaviour<EnemyManagerP2>
{

    public GameObject enemyPrefab;

    public GameObject[] spawnPoints;

    float waveNum = 0;

    int enemiesSpawning;
    int currentEnemyCount;

    float timeBetweenWaves = 5;

    public enum WaveStatus { Fight, Wait, Lost}
    
    WaveStatus waveStatus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(waveStatus)
        {
            //enemies have spawned and are being fought
            case WaveStatus.Fight:
                break;
            //break between waves
            case WaveStatus.Wait:
                break;
            //if player dies, destroy all enemies
            case WaveStatus.Lost:
                break;
        }


    }

    void SpawnEnemies()
    {
        //have wait period

        //for loop to spawn enemies by random number in spawnPoints[] and iterate until enemiesSpawning

        //check if enemies are all killed

        //then increase emeimes spawning
        //increase wave;

        //repeat


    }
}
