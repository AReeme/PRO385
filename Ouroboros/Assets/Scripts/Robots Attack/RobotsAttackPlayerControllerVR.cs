using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsAttackPlayerController : MonoBehaviour
{
    public RobotsAttackVRManager GameManager;

    private void Awake()
    {
        GameManager = gameObject.GetComponent<RobotsAttackVRManager>();
    }

    void Start()
    {
        GameManager = FindObjectOfType<RobotsAttackVRManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDamage(int damage = 10)
    {
        if (GameManager != null)
        {
            GameManager.TakeDamage(damage);
        }
    }
}
