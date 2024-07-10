using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerVR : GameManagerVRBase
{
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

