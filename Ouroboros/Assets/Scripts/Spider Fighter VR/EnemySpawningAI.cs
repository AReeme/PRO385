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


        // Instantiates Spiders at Randomized Spawn Points
        for (int i = 0; i < spawnNumber; i++)
        {
            spawnPointIndex = Mathf.RoundToInt(Random.Range(1, spawnPoints.Length));

            float radius = 10f;
            originPoint = spawnPoints[spawnPointIndex - 1].transform.position;
            float xPos = originPoint.x += Random.Range(-radius, radius);
            float yPos = originPoint.y;
            float zPos = originPoint.z += Random.Range(-radius, radius);

            randomPosition = new Vector3(xPos, yPos, zPos);

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
    }

    IEnumerator SpawnCoroutine()
    {
        running = false;
        yield return new WaitForSeconds(10f);
        SpawnSpider();
        running = true;
    }
}
