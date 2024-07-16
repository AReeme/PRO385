using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawningAI;

public class EnemyAI : MonoBehaviour
{
    EnemySpawningAI enemySpawningAI;

    GameObject player;

    GameObject bulletPrefab;

    // Stats
    float speed;
    float health;
    float maxHealth;
    float damage;
    float bulletDamage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAt = player.transform.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);

        switch (enemySpawningAI.type)
        {
            case EnemySpawningAI.enemyTypes.Basic:
                Vector3.MoveTowards(this.gameObject.transform.position, player.transform.position, speed);
                break;
            case EnemySpawningAI.enemyTypes.Long_Leg:
                Vector3.MoveTowards(this.gameObject.transform.position, player.transform.position, speed);
                break;
            case EnemySpawningAI.enemyTypes.Black_Widow:
                Vector3.MoveTowards(this.gameObject.transform.position, player.transform.position, speed);
                break;
        }
    }

    void SetStats()
    {
        // Sets Stats
        switch (enemySpawningAI.type)
        {
            case enemyTypes.Basic:
                maxHealth = 30;
                speed = 5;
                damage = 10;
                break;
            case enemyTypes.Long_Leg:
                maxHealth = 15;
                speed = 10;
                damage = 7.5f;
                break;
            case enemyTypes.Black_Widow:
                maxHealth = 25;
                speed = 2.5f;
                damage = 25;
                bulletDamage = 5;
                break;
        }
    }

    void OnColliderEnter(Collider other)
    {
        //player.health -= damage
        Destroy(this.gameObject);
    }
}
