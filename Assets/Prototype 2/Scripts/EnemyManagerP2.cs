using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyManagerP2 : GameBehaviour<EnemyManagerP2>
{

    public GameObject enemyPrefab;

    public GameObject[] spawnPoints;

    public float waveNum = 1;

    public float enemyHealth = 20;
    public float enemyDmg = 2;

    int enemiesSpawning = 0  ;
    int currentEnemyCount;

    float timeBetweenWaves = 3;

    public enum WaveStatus { Fight, Wait, Lost}
    
    public WaveStatus waveStatus;
    bool spawned;

    public TMP_Text waveNumText;
    public GameObject waveComplete;
    

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
                waveComplete.SetActive(false);

                if (!spawned)
                {
                    waveNum++;
                    waveNumText.text = "Wave " + waveNum;
                    enemiesSpawning += 2;
                    SpawnEnemies();
                    spawned = true;
                }

                CheckEnemies();
                if(currentEnemyCount == 0)
                {
                    waveStatus = WaveStatus.Wait;
                }
                break;
            //break between waves
            case WaveStatus.Wait:
                waveComplete.SetActive(true);
                spawned = false;
                _PC2.playerHealth = _PC2.playerHealthMax;
                print("Wave status waiting");
                StartCoroutine(Fight());
                break;
            //if player dies, destroy all enemies
            case WaveStatus.Lost:
                _GM2.gameState = prototype2.GameManagerP2.GameState.Gameover;

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
