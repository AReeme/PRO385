using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
	NavMeshAgent agent;
	[SerializeField] LayerMask groundLayer;
	[SerializeField] Animator anim;

	// Patrol
	Vector3 destination;
	bool walkPointSet;
	[SerializeField] float range;
	bool isMovingToDestination;
	float startTime;

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.speed = 1.0f;
	}

	// Update is called once per frame
	void Update()
	{
		Patrol();
	}

	void Patrol()
	{
		if (!walkPointSet && !isMovingToDestination)
		{
			StartCoroutine(WaitAndSearch());
			SearchForDest();
		}

		if (walkPointSet)
		{
			agent.SetDestination(destination);
			anim.SetBool("IsWalking", true);
		}

		// Check if the agent is stuck or has reached the destination
		if (agent.velocity.sqrMagnitude < 0.01f && isMovingToDestination)
		{
			startTime += Time.deltaTime;
		}
		else
		{
			startTime = 0;
		}

		if (startTime > 5)
		{
			Debug.Log("Agent stuck, searching for a new destination.");
			walkPointSet = false;
			isMovingToDestination = false;
			SearchForDest();
		}

		if (Vector3.Distance(transform.position, destination) < 1)
		{
			Debug.Log("Reached destination");
			walkPointSet = false;
			isMovingToDestination = false;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (this.gameObject.CompareTag("Child") && other.gameObject.CompareTag("Bullet"))
		{
			Destroy(other.gameObject);
			Destroy(this.gameObject);
		}
	}

	void SearchForDest()
	{
		float x, z;
		Vector3 potentialDestination;

		do
		{
			x = Random.Range(-range, range);
			z = Random.Range(-range, range);
			potentialDestination = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
		} while (!IsValidDestination(potentialDestination));

		destination = potentialDestination;
		walkPointSet = true;
		isMovingToDestination = true;
	}

	bool IsValidDestination(Vector3 target)
	{
		NavMeshHit hit;
		return NavMesh.SamplePosition(target, out hit, 1.0f, NavMesh.AllAreas);
	}

	IEnumerator WaitAndSearch()
	{
		yield return new WaitForSeconds(10);
		anim.SetBool("IsWalking", false);
		SearchForDest();
	}

	
}