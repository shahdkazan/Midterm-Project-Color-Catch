using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerTransform;  // Reference to the player
    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent
        if (navMeshAgent == null)
        {
            Debug.LogError("No NavMeshAgent found"); // Warn if missing
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            navMeshAgent.SetDestination(playerTransform.position); // Move towards player
        }
    }
}
