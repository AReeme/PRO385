using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMissle : MonoBehaviour
{
	[SerializeField] private float speed = 20f;
	[SerializeField] private float lifetime = 5f;

	void Start()
	{
		Destroy(gameObject, lifetime);
	}

	void Update()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
