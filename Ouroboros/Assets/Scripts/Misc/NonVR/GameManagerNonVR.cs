using UnityEngine;

public class GameManagerNonVR : GameManagerVRBase
{
    protected override void InitializeGame()
    {
        base.InitializeGame();
        // Initialize non-VR specific logic
        Debug.Log("Non-VR Game Initialized");
    }

    protected override void HandleGameUpdate()
    {
        base.HandleGameUpdate();
    }

}
