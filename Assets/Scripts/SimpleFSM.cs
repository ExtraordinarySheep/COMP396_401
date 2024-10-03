using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum EnemySimpleFSMStates
{
    Patrol,
    Chase,
    Attack,
    FleeToHQ,
    InvestigateLastLocation,
    Looking
}

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleFSM : MonoBehaviour
{
    [SerializeField] private EnemySimpleFSMStates _currentState;
    [SerializeField] private GameObject _player;
    [SerializeField] private List<Transform> _patrollingLocations;
    [SerializeField] private GameObject _HQ;

    [Header("Guard Stats")]
    [SerializeField] private float _distanceToChase = 10f;
    [SerializeField] private float _distanceToAttack = 3f;
    [SerializeField] private float _runSpeed = 6f, _walkSpeed = 3f;
    private NavMeshAgent _agent;
    private int _patrolIndex;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentState = EnemySimpleFSMStates.Patrol;
        _player = GameObject.FindWithTag("Player");
        _patrollingLocations = _patrollingLocations.Where(p => p != transform).ToList();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemySimpleFSMStates.Patrol: Patrol(); break;
            case EnemySimpleFSMStates.Chase: Chase(); break;
            case EnemySimpleFSMStates.Attack: Attack(); break;
            case EnemySimpleFSMStates.FleeToHQ: FleeToHQ(); break;
            case EnemySimpleFSMStates.InvestigateLastLocation: InvestigateLastLocation(); break;
            case EnemySimpleFSMStates.Looking: Looking(); break;
        }
    }

    private void Patrol()
    {
        SetDestination(_patrollingLocations[_patrolIndex].position, _walkSpeed);

        if (ReachedDestination())
        {
            _patrolIndex = (_patrolIndex + 1) % _patrollingLocations.Count;
            TransitionToState(EnemySimpleFSMStates.Looking);
        }

        if (IsPlayerInRange(_distanceToChase))
            TransitionToState(EnemySimpleFSMStates.Chase);
    }

    private void Chase()
    {
        SetDestination(_player.transform.position, _runSpeed);

        if (!IsPlayerInRange(_distanceToChase))
            TransitionToState(EnemySimpleFSMStates.InvestigateLastLocation);

        if (IsPlayerInRange(_distanceToAttack))
            TransitionToState(EnemySimpleFSMStates.Attack);
    }

    private void Attack()
    {
        if (!IsPlayerInRange(_distanceToAttack))
            TransitionToState(EnemySimpleFSMStates.Chase);
    }

    private void FleeToHQ()
    {
        SetDestination(_HQ.transform.position, _runSpeed);

        if (ReachedDestination())
            TransitionToState(EnemySimpleFSMStates.Looking);
    }

    private void InvestigateLastLocation()
    {
        TransitionToState(EnemySimpleFSMStates.Patrol);
    }

    private void Looking()
    {
        StartCoroutine(LookAround());
    }

    private IEnumerator LookAround()
    {
        yield return new WaitForSeconds(2f);
        TransitionToState(EnemySimpleFSMStates.Patrol);
    }


    private bool IsPlayerInRange(float range) =>
        Vector3.Distance(transform.position, _player.transform.position) < range;

    private bool ReachedDestination() =>
        !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;

    private void SetDestination(Vector3 destination, float speed)
    {
        _agent.speed = speed;
        _agent.SetDestination(destination);
    }

    private void TransitionToState(EnemySimpleFSMStates state) => _currentState = state;
}
