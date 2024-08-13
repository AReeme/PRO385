using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletDamage = 5;
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
	void OnColliderEnter(Collider other)
    {
		if(other.tag == "Enemy")
		{
		  Destroy(this.gameObject);
		}
        //player.health -= bulletDamage
    }
}
