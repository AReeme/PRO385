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

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			Collider[] hitcolliders = Physics.OverlapSphere(gameObject.transform.position, 30, default);
			foreach(var hitcollider in hitcolliders)
			{ 
				if(hitcollider.gameObject.tag == "Enemy")
				{
					hitcollider.gameObject.GetComponent<EnemyAI>().TakeDamage(100);
				}
			}

		}
	}
}
