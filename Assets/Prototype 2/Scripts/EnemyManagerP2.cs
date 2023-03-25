using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerP2 : GameBehaviour<EnemyManagerP2>
{

    public GameObject enemyPrefab;

    public GameObject[] spawnPoints;

    float waveNum = 0;

    public float enemyHealth = 20;
    public float enemyDmg = 2;

    int enemiesSpawning = 5;
    int currentEnemyCount;

    float timeBetweenWaves = 1;

    public enum WaveStatus { Fight, Wait, Lost}
    
    WaveStatus waveStatus;
    bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
        waveStatus = WaveStatus.Fight;
    }

    // Update is called once per frame
    void Update()
    {
        switch(waveStatus)
        {
            //enemies have spawned and are being fought
            case WaveStatus.Fight:
                
                if(!spawned)
                {
                    SpawnEnemies();
                    spawned = true;
                }

                CheckEnemies();
                if(currentEnemyCount == 0)
                {
                    waveNum++;
                    enemiesSpawning += 2;
                    waveStatus = WaveStatus.Wait;
                }
                break;
            //break between waves
            case WaveStatus.Wait:
                print("Wave status waiting");
                spawned = true;
                StartCoroutine(Fight());
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
        for (int i = 0; i < enemiesSpawning; i++)
        {
            print("spawing");
            Instantiate(enemyPrefab, GenerateSpawnPosition().transform.position, enemyPrefab.transform.rotation);
        }


        //check if enemies are all killed

        //then increase emeimes spawning
        //increase wave;

        //repeat


    }

    void CheckEnemies()
    {
        currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        

    }

    IEnumerator Fight()
    {
        print("waiting");
        yield return new WaitForSeconds(timeBetweenWaves);
        
        waveStatus = WaveStatus.Fight;
    }

    GameObject GenerateSpawnPosition()
    {

        return spawnPoints[RandomIntBetweenTwoInt(0, spawnPoints.Length)];

    }
}
