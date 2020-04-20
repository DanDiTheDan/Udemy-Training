using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] enemies;

    private float timeBtwSpawns;
    public float startTimeBtwSpawns;

    // Update is called once per frame
    void Update()
    {
        if(timeBtwSpawns <= 0)
        {
            Transform randomSpawnpoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomGameObject = enemies[Random.Range(0, enemies.Length)];
            Instantiate(randomGameObject, randomSpawnpoint);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
