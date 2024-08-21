using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOEnemyAI : MonoBehaviour
{
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

    UFOSpawningAI enemySpawningAI;
    public GameObject UFOFront;
    GameObject player;
    public GameObject explosion;
    public GameObject bulletPrefab;
    public GameObject deathSound;
    bool running;
    Vector3 lookAt;

    // Stats
    float speed;
    float health;
    float maxHealth;
    float damage;
    float bulletSpeed = 15f;
    float hoverHeight = 0.5f;
    float hoverSpeed = 1f;
    bool isHovering = false;
    bool hasStopped = false;
    Vector3 originalPosition;
    Rigidbody rb;

    // Bounce properties
    public float bounceForce = 5f;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawningAI = FindObjectOfType<UFOSpawningAI>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        running = true;
        SetStats();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            lookAt = player.transform.position;
            lookAt.y = transform.position.y;
            Quaternion targetRotation = Quaternion.LookRotation(lookAt - transform.position);
            Quaternion additionalRotation = Quaternion.Euler(0, 90, 0);
            targetRotation *= additionalRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);

            if (!hasStopped)
            {
                if (Vector3.Distance(transform.position, player.transform.position) > 1f)
                {
                    MoveTowardsPlayer();
                    isHovering = false;
                }
                else
                {
                    Hover();
                    isHovering = true;
                    hasStopped = true;
                }
            }
            else
            {
                Hover();
            }

            if (running) StartCoroutine(SpawnCoroutine());
        }
    }

    void SetStats()
    {
        // Sets Stats
        maxHealth = 30;
        speed = 0.5f;
        damage = 10;
    }

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			//player.health -= damage;
			enemySpawningAI.Score += 10;
			Destroy(this.gameObject);
		}
		else if (other.gameObject.CompareTag("Wall"))
		{
			//Vector3 bounceDirection = Vector3.Reflect(transform.forward, other.contacts[0].normal);
			//rb.velocity = bounceDirection * bounceForce;
		}
		else if (other.gameObject.CompareTag("Bullet"))
		{
            Destroy(other.gameObject);
            TakeDamage(other.gameObject.GetComponent<Bullet>().bulletDamage);
		}
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
        GameObject bulletShot = Instantiate(bulletPrefab, UFOFront.transform.position, Quaternion.LookRotation(lookAt - UFOFront.transform.position));
        Rigidbody bulletRb = bulletShot.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = (lookAt - UFOFront.transform.position).normalized * bulletSpeed;
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        originalPosition = transform.position;
    }

    void Hover()
    {
        Vector3 hoverPosition = originalPosition;
        hoverPosition.y += Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = hoverPosition;
    }

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			enemySpawningAI.UpdateScore();
            enemySpawningAI.EnemyDestroyed();
            Instantiate(explosion, transform.position, transform.rotation);
            //Instantiate(deathSound, transform.position, transform.rotation);
            Destroy(this.gameObject);
		}
	}
}
