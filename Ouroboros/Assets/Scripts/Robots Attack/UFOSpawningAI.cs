using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawningAI : MonoBehaviour
{

    private void OnEnable()
    {
        GameManagerVRBase.OnGameEnd += HandleGameEnd;
        GameManagerVRBase.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        GameManagerVRBase.OnGameEnd -= HandleGameEnd;
        GameManagerVRBase.OnGameOver -= HandleGameOver;
    }

    private void HandleGameEnd()
    {
        StopAllCoroutines();
    }
    private void HandleGameOver()
    {
        StopAllCoroutines();
    }

    public enum enemyTypes
    {
        UFO
    }

    // Prefabs
    [SerializeField] GameObject UFO;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] RobotsAttackVRManager manager;

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
    private int currentEnemies;
    public bool DiffChanged = false;

    void Start()
    {
        manager = FindAnyObjectByType<RobotsAttackVRManager>();
        running = false;
        TotalSpidersSpawned = 0;
        Score = 0;
        Difficulty = 1;
        currentEnemies = 0;
        StartSpawning();
    }

    void Update()
    {
        if (running)
        {
            StartCoroutine(SpawnCoroutine());
        }
        CheckEnemyCount();
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

        int maxSpawnNumber = Difficulty;
        int spawnCount = Mathf.Min(spawnNumber, maxSpawnNumber - currentEnemies);

        type = enemyTypes.UFO;

        for (int i = 0; i < spawnCount; i++)
        {
            spawnPointIndex = Mathf.RoundToInt(Random.Range(0, spawnPoints.Length));

            float radius = 1f;
            originPoint = spawnPoints[spawnPointIndex].transform.position;
            float xPos = originPoint.x + Random.Range(-radius, radius);
            float yPos = originPoint.y;
            float zPos = originPoint.z + Random.Range(-radius, radius);

            randomPosition = new Vector3(xPos, yPos, zPos);

            Instantiate(UFO, randomPosition, Quaternion.identity);

            TotalSpidersSpawned++;

            currentEnemies++;
            DiffChanged = false;
        }
    }

    public void CheckEnemyCount()
    {
        if (currentEnemies == 0 && DiffChanged)
        {
            manager.Difficulty += 1;
            DiffChanged = true;
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

    public void UpdateScore(int score = 50)
    {
        Score += score;
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}
