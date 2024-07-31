using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsAttackPlayerControllerVR : PlayerControllerScript
{
    public float playerHealth;
    public int score;

    public SpiderFighterVRManager GameManager;

    private void Awake()
    {
        GameManager = gameObject.GetComponent<SpiderFighterVRManager>();

        base.Awake();
    }

    void Start()
    {
        GameManager = FindObjectOfType<SpiderFighterVRManager>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        UpdateHP();

    }


    void UpdateHP()
    {
        if (GameManager.PlayerHealth != playerHealth)
        {
            GameManager.PlayerHealth = playerHealth;
        }
        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        GameManager?.PauseGame();
    }
}
