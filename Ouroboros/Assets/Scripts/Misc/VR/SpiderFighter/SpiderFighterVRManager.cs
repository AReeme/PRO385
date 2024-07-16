using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFighterVRManager : GameManagerVRBase
{
    public int PlayerScore;
    public float PlayerHealth;

    public int Difficulty;
    public int EnemyCount;

    protected override void InitializeGame()
    {
        base.InitializeGame();
        // Initialize VR specific logic
        Debug.Log("VR Game Initialized");
    }

    protected override void HandleGameUpdate()
    {
        base.HandleGameUpdate();
    }
}
