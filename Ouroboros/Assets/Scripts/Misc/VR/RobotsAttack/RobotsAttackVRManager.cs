using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RobotsAttackVRManager : GameManagerVRBase
{
    public int PlayerScore;
    public float PlayerHealth;

    public GameObject HealthUI;
    public GameObject ScoreUI;

    public int Difficulty;
    public int EnemyCount;

    public UFOSpawningAI enemySpawner;
    public DataStorageUI dataStorageUI;

    public void Start()
    {
        InitializeGame();
        PlayerHealth = 100;
        Difficulty = 1;
    }

    protected override void InitializeGame()
    {
        base.InitializeGame();
        Debug.Log("VR Game Initialized");

        // Find the EnemySpawningAI component in the scene
        enemySpawner = FindObjectOfType<UFOSpawningAI>();
        dataStorageUI = FindObjectOfType<DataStorageUI>();

        if (enemySpawner != null)
        {
            StartCoroutine(SpawnCoroutine());
        }
    }

    protected override void HandleGameUpdate()
    {
        base.HandleGameUpdate();

        if (enemySpawner != null)
        {
            EnemyCount = enemySpawner.TotalSpidersSpawned;
            PlayerScore = enemySpawner.Score;
            UpdateUI();
        }

        if (PlayerScore >= 500)
        {
            EndGame();
        }

        if (PlayerHealth <= 0)
        {
            GameOver();
        }

        

    }

    public override void PauseGame()
    {
        base.PauseGame();
        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }
    }

    public override void ResumeGame()
    {
        base.ResumeGame();
        if (enemySpawner != null)
        {
            enemySpawner.StartSpawning();
        }
    }

    public override void EndGame()
    {
        base.EndGame();
        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }
    }

    public override void GameOver()
    {
        base.EndGame();
        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }
    }

    public void Update()
    {
        HandleGameUpdate();

        if (enemySpawner != null)
        {
            enemySpawner.Difficulty = Difficulty;
        }
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(5f);
        enemySpawner.StartSpawning();
    }

    public void UpdateUI()
    {
        dataStorageUI.HealthUI.text = "Health: " + PlayerHealth;
        dataStorageUI.ScoreUI.text = "Score: " + PlayerScore;
    }
}
