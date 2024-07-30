using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawningAI;

public class UFOSpawningAI : MonoBehaviour
{
    public enum enemyTypes
    { 
        UFO
    }

    // Prefabs
    [SerializeField] GameObject UFO;
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
        Difficulty = 2;
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
        spawnNumber = Mathf.RoundToInt(Random.Range(1, 5));

        spawnNumber = spawnNumber * Difficulty;

        type = enemyTypes.UFO;

        for (int i = 0; i < spawnNumber; i++)
        {
            spawnPointIndex = Mathf.RoundToInt(Random.Range(1, spawnPoints.Length));

            float radius = 10f;
            originPoint = spawnPoints[spawnPointIndex - 1].transform.position;
            float xPos = originPoint.x + Random.Range(-radius, radius);
            float yPos = originPoint.y;
            float zPos = originPoint.z + Random.Range(-radius, radius);

            randomPosition = new Vector3(xPos, yPos, zPos);

            Instantiate(UFO, randomPosition, Quaternion.identity);

            TotalSpidersSpawned++;
            
            Difficulty = Mathf.RoundToInt(Mathf.Pow(Difficulty, 1.3f));
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
