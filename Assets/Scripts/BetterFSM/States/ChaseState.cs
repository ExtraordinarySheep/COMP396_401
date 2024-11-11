using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : BaseState
{
    private readonly GameObject _player;
    public ChaseState(NavMeshAgent agent, GameObject player, Animator animator) : base(agent, animator)
    {
        _player = player;
    }

    public override void OnEnter()
    {
        Debug.Log("Time to DIE");
        agent.speed = 4f;
        animator.Play(Run);
    }

    public override void Update()
    {
        agent.SetDestination(_player.transform.position);
    }

    public override void OnExit()
    {
        Debug.Log("Leaving chase, something else has priority");
    }
}
