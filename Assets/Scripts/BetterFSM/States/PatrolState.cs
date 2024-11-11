using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{
    private GameObject[] _patrolPoints;
    private int _index;
    
    public PatrolState(NavMeshAgent agent, Animator animator, GameObject[] patrolPoints) 
        : base(agent, animator)
    {
        _patrolPoints = patrolPoints;
        _index = 0;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Patrol mode");
        agent.speed = 1.3f;
        animator.Play(Walk);
    }

    public override void Update()
    {
        Debug.Log($"Patrolling points that I know of");
        Vector3 destination = _patrolPoints[_index].transform.position;
        agent.SetDestination(destination);

        if (Vector3.Distance(agent.transform.position, destination) < 2.0f)
        {
            _index = (_index + 1) % _patrolPoints.Length;
        }
    }

    public override void OnExit()
    {
        Debug.Log("Leaving patrol to do something else");
    }
}
