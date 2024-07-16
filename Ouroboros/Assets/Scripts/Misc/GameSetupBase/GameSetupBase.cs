using UnityEngine;
using UnityEngine.XR;

public class GameSetupBase : MonoBehaviour
{
    public GameManagerVRBase gameManager;

    public bool Debug;

    void Awake()
    {
        if (!Debug)
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
        else
        {
            gameManager = gameObject.AddComponent<SpiderFighterVRManager>();
        }
    }
}