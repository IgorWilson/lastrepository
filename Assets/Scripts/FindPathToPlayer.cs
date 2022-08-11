using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FindPathToPlayer : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Transform _chaseTarget;
    [SerializeField] private float _stopSearchingDistance = 1f;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _chaseTarget.position) <= _stopSearchingDistance)
            StopMove();
        else
            StartMove();
    }

    public void StopMove()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    public void StartMove()
    {
        agent.isStopped = false;
        agent.SetDestination(_chaseTarget.position);
    }


}