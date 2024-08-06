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
            gameManager = gameObject.AddComponent<RobotsAttackVRManager>();
        }
        else
        {
            gameManager = gameObject.AddComponent<SpiderFighterVRManager>();
        }
    }
}