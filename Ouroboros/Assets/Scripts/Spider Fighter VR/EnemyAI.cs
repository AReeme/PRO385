using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawningAI;

public class EnemyAI : MonoBehaviour
{
    EnemySpawningAI enemySpawningAI;
    GameObject player;
    GameObject bulletPrefab;
    bool running;
    Vector3 lookAt;

    private void OnEnable()
    {
        GameManagerVRBase.OnGameEnd += HandleGameEnd;
        GameManagerVRBase.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        GameManagerVRBase.OnGameEnd -= HandleGameEnd;
        GameManagerVRBase.OnGameOver -= HandleGameOver;
    }

    private void HandleGameEnd()
    {
        Destroy(this.gameObject);
    }

    private void HandleGameOver()
    {
        Destroy(this.gameObject);
    }

    // Stats
    float speed;
    float health;
    float maxHealth;
    float damage;
    float bulletSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawningAI = FindAnyObjectByType<EnemySpawningAI>();
        player = GameObject.FindGameObjectWithTag("Player");
        running = true;
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        lookAt = player.transform.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);

        switch (enemySpawningAI.type)
        {
            case EnemySpawningAI.enemyTypes.Basic:
                MoveTowardsPlayer();
                break;
            case EnemySpawningAI.enemyTypes.Long_Leg:
                MoveTowardsPlayer();
                break;
            case EnemySpawningAI.enemyTypes.Black_Widow:
                MoveTowardsPlayer();
                if (running) SpawnCoroutine();
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
                speed = 0.5f;
                damage = 10;
                break;
            case enemyTypes.Long_Leg:
                maxHealth = 15;
                speed = 1;
                damage = 7.5f;
                break;
            case enemyTypes.Black_Widow:
                maxHealth = 25;
                speed = 0.25f;
                damage = 25;
                break;
        }
    }

    void OnColliderEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //player.health -= damage
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shield"))
        {
            enemySpawningAI.UpdateScore();
            Destroy(this.gameObject);
        }

        //enemySpawningAI.TotalSpidersSpawned -=1;
    }

    IEnumerator SpawnCoroutine()
    {
        running = false;
        yield return new WaitForSeconds(10f);
        SpawnBullet(); 
        running = true;
    }

    void SpawnBullet()
    {
        GameObject bulletShot = Instantiate(bulletPrefab, this.gameObject.transform.position, Quaternion.LookRotation(lookAt));
        bulletShot.transform.position = Vector3.MoveTowards(bulletShot.transform.position, player.transform.position, bulletSpeed);
    }

    void MoveTowardsPlayer()
    {
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
