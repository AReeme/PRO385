using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform tankPosition;
    public float lifetime = 5f;
    void Start()
    {
        StartCoroutine("DestroyGameObject", lifetime);
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = FindObjectOfType<TankController>().gameObject.transform.position;
    }

	public void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
	}
}
