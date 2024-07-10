using UnityEngine;
using UnityEngine.XR;

public class GameSetupBase : MonoBehaviour
{
    public GameManagerVRBase gameManager;

    void Awake()
    {
        // Choose the appropriate game manager
        if (XRSettings.isDeviceActive)
        {
            gameManager = gameObject.AddComponent<GameManagerVR>();
        }
        else
        {
            gameManager = gameObject.AddComponent<GameManagerNonVR>();
        }
    }
}