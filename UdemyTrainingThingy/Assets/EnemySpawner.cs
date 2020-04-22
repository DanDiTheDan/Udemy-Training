using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool alertMode;
    public bool alertActivated;
    public bool alertPauseActive;
    public float alertTimer;
    public float alertPause = 0f;
    public int randomInt;
    public int alertChance = 10;

    public Transform[] spawnPoints;
    public GameObject[] enemies;

    public float timeBtwSpawns;
    public float startTimeBtwSpawns;

    PlayerController playerScript;
    public GameObject player;
    // Update is called once per frame
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        if(player != null)
        {
            if(timeBtwSpawns <= 0)
            {
                Transform randomSpawnpoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject randomGameObject = enemies[Random.Range(0, enemies.Length)];
            
                Instantiate(randomGameObject, randomSpawnpoint);
                if (alertMode == false && alertActivated == true && alertPause <= 0)
                {
                    alertPauseActive = false;
                    randomInt = Random.Range(0, alertChance + 1);
                    if (randomInt == alertChance)
                    {
                        alertMode = true;
                        alertTimer = Random.Range(3, 8);
                    }
                }
                if (startTimeBtwSpawns > 0.6f)
                {
                    startTimeBtwSpawns -= playerScript.GetScore() / (startTimeBtwSpawns * 500 * startTimeBtwSpawns);
                }
                else
                {
                    if (alertMode == true)
                    {
                        startTimeBtwSpawns = 0.2f;
                        if (alertTimer <= 0)
                        {
                            alertMode = false;
                            alertPause = 10f;
                            alertPauseActive = true;
                        }
                    }
                    else
                    { 
                        startTimeBtwSpawns = 0.6f;
                        alertActivated = true;
                    }
                }
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
                if (alertPauseActive == true)
                {
                    alertPause -= Time.deltaTime;
                }
                if (alertMode == true)
                {
                    alertTimer -= Time.deltaTime;
                }
            }
        }
    }
}