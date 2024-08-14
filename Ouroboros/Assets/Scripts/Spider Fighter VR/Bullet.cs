using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float bulletDamage = 5;
	[SerializeField] private float speed = 20f;
	[SerializeField] private float lifetime = 5f;
	[SerializeField] public bool isMissle = false;

	void Start()
	{
		Destroy(gameObject, lifetime);
	}

	void Update()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
