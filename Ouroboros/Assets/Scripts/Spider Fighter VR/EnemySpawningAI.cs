using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningAI : MonoBehaviour
{
    public enum enemyTypes
    {
        Basic,
        Long_Leg,
        Black_Widow
    }

    // Prefabs
    [SerializeField] GameObject player;
    [SerializeField] GameObject basicSpider;
    [SerializeField] GameObject longLegSpider;
    [SerializeField] GameObject blackSpider;
    [SerializeField] GameObject[] spawnPoints;

    // Stats
    enemyTypes type;
    float speed;
    float health;
    float maxHealth;
    float damage;
    float bulletDamage;

    // Miscellaneous
    int randomNumber;
    int spawnNumber;
    int spawnPointIndex;
    bool running;

    // Start is called before the first frame update
    void Start()
    {
        // Spawns Random Spider
        SpawnSpider();

        // Bool that checks to see if Coroutine is available to run
        running = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Spiders Spawn every ten Seconds
        if (running) StartCoroutine(SpawnCoroutine());
    }

    void SpawnSpider()
    {
        // Randomizes which Spider to Spawn
        randomNumber = Mathf.RoundToInt(Random.Range(1, 3));

        // Randomizes how many spiders to spawn
        spawnNumber = Mathf.RoundToInt(Random.Range(1, 5));

        // Sets Enemy Type
        switch (randomNumber)
        {
            case 1:
                type = enemyTypes.Basic;
                break;
            case 2:
                type = enemyTypes.Long_Leg;
                break;
            case 3:
                type = enemyTypes.Black_Widow;
                break;
        }

        // Sets Stats
        switch (type)
        {
            case enemyTypes.Basic:
                maxHealth = 30;
                speed = 5;
                damage = 10;
                break;
            case enemyTypes.Long_Leg:
                maxHealth = 15;
                speed = 10;
                damage = 7.5f;
                break;
            case enemyTypes.Black_Widow:
                maxHealth = 25;
                speed = 2.5f;
                damage = 25;
                bulletDamage = 5;
                break;
        }

        // Instantiates Spiders at Randomized Spawn Points
        for (int i = 0; i < spawnNumber; i++)
        {
            spawnPointIndex = Mathf.RoundToInt(Random.Range(1, spawnPoints.Length));

            switch (type)
            {
                case enemyTypes.Basic:
                    Instantiate(basicSpider, spawnPoints[spawnPointIndex - 1].transform.position, Quaternion.identity);
                    break;
                case enemyTypes.Long_Leg:
                    Instantiate(longLegSpider, spawnPoints[spawnPointIndex - 1].transform.position, Quaternion.identity);
                    break;
                case enemyTypes.Black_Widow:
                    Instantiate(blackSpider, spawnPoints[spawnPointIndex - 1].transform.position, Quaternion.identity);
                    break;
            }
        }

        health = maxHealth;
    }

    IEnumerator SpawnCoroutine()
    {
        running = false;
        yield return new WaitForSeconds(10f);
        SpawnSpider();
        running = true;
    }
}
