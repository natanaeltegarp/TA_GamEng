using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private UnityEngine.AI.NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component

    void Start()
    {
        // Get the NavMeshAgent component attached to this GameObject
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Check if the player has been assigned
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned in the inspector.");
        }
    }

    void Update()
    {
        // If player is assigned, set the agent's destination to the player's position
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}
