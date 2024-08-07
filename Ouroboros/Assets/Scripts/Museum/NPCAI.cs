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
            startTime = Time.time;
        }

        if (Vector3.Distance(transform.position, destination) < 1)
        {
            walkPointSet = false;
            isMovingToDestination = false;
            Debug.Log("Reached destination");
        }
        else if (Time.time - startTime > 5 && !isMovingToDestination) 
        {
            SearchForDest(); 
        }
    }

    void SearchForDest()
    {
        float x;
        float z;

        do
        {
            x = Random.Range(-range, range);
            z = Random.Range(-range, range);
        } while ((x >= -5 && x <= 5) || (z >= -5 && z <= 5));

        destination = new Vector3(transform.position.x + x + 50, transform.position.y, transform.position.z + z + 50);

        if (Physics.Raycast(destination, Vector3.down, groundLayer))
        {
            walkPointSet = true;
            isMovingToDestination = true;
        }
    }

    IEnumerator WaitAndSearch()
    {
        yield return new WaitForSeconds(10);
        anim.SetBool("IsWalking", false);
        SearchForDest();
    }
}
