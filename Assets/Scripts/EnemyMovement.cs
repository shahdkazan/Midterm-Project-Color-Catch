using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour

{
    public Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("no navmesh agent found");

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }

    }
}
