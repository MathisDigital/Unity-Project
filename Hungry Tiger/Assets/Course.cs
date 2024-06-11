using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshMovement : MonoBehaviour
{
    public float moveRadius = 10f; // Rayon de la zone de mouvement aléatoire
    public float waitTime = 2f; // Temps d'attente avant de se déplacer à une nouvelle position

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing from this game object.");
        }
        else
        {
            StartCoroutine(MoveToRandomPosition());
        }
    }

    IEnumerator MoveToRandomPosition()
    {
        while (true)
        {
            Vector3 randomPosition = GetRandomPositionOnNavMesh();
            agent.SetDestination(randomPosition);

            // Attendre jusqu'à ce que l'agent atteigne la destination ou abandonne
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // Attendre un certain temps avant de se déplacer à une nouvelle position
            yield return new WaitForSeconds(waitTime);
        }
    }

    Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, moveRadius, 1);
        return hit.position;
    }
}
