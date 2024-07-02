using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer;

    //Patrol
    Vector3 destination;
    bool walkPointSet;
    [SerializeField] float range;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (!walkPointSet)  SearchForDest();

        if (walkPointSet) agent.SetDestination(destination);
        if (Vector3.Distance(transform.position, destination) < 10) walkPointSet = false;
    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destination = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destination, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
    }
}
