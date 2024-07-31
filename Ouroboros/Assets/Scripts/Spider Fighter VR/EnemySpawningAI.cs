using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningAI : MonoBehaviour, IGameComponent
{
    public enum enemyTypes
    {
        Basic,
        Long_Leg,
        Black_Widow
    }

    // Prefabs
    [SerializeField] GameObject basicSpider;
    [SerializeField] GameObject longLegSpider;
    [SerializeField] GameObject blackSpider;
    [SerializeField] GameObject[] spawnPoints;

    public enemyTypes type;

    // Miscellaneous
    int randomNumber;
    int spawnNumber;
    int spawnPointIndex;
    bool running;
    Vector3 originPoint;
    Vector3 randomPosition;

    public int TotalSpidersSpawned { get; set; }
    public int Score { get; set; }
    public int Difficulty { get; set; }

    void Start()
    {
        running = false;
        TotalSpidersSpawned = 0;
        Score = 0;
        Difficulty = 1;
    }

    void Update()
    {
        if (running)
        {
            StartCoroutine(SpawnCoroutine());
        }
    }

    public void StartSpawning()
    {
        running = true;
    }

    public void StopSpawning()
    {
        running = false;
        StopAllCoroutines();
    }

    void SpawnSpider()
    {
        randomNumber = Mathf.RoundToInt(Random.Range(1, 3));
        spawnNumber = Mathf.RoundToInt(Random.Range(1, 5));

        spawnNumber = spawnNumber * Difficulty;

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

        for (int i = 0; i < spawnNumber; i++)
        {
            spawnPointIndex = Mathf.RoundToInt(Random.Range(1, spawnPoints.Length));

            float radius = 10f;
            originPoint = spawnPoints[spawnPointIndex - 1].transform.position;
            float xPos = originPoint.x + Random.Range(-radius, radius);
            float yPos = originPoint.y;
            float zPos = originPoint.z + Random.Range(-radius, radius);

            randomPosition = new Vector3(xPos, yPos, zPos);

            switch (type)
            {
                case enemyTypes.Basic:
                    Instantiate(basicSpider, randomPosition, Quaternion.identity);
                    break;
                case enemyTypes.Long_Leg:
                    Instantiate(longLegSpider, randomPosition, Quaternion.identity);
                    break;
                case enemyTypes.Black_Widow:
                    Instantiate(blackSpider, randomPosition, Quaternion.identity);
                    break;
            }
            TotalSpidersSpawned++;

            Difficulty++;
        }
    }

    IEnumerator SpawnCoroutine()
    {
        running = false;
        yield return new WaitForSeconds(10f);
        SpawnSpider();
        running = true;
    }

    // IGameComponent methods
    public void Initialize()
    {
        // Initialize if needed
    }

    public void UpdateComponent()
    {
        Update();
    }

    public void Pause()
    {
        StopSpawning();
    }

    public void Resume()
    {
        StartSpawning();
    }

    public void End()
    {
        StopSpawning();
    }

    public void UpdateScore(int score = 10)
    {
        Score += score;
    }
}
