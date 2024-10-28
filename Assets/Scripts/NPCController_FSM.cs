using UnityEngine;

public class NPCController_FSM : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Return
    }

    public State currentState = State.Idle;
    public Transform playerTransform; 
    public float detectionRadius = 10.0f;
    public float attackRadius = 2.0f;
    public float moveSpeed = 5.0f;

    private Rigidbody npcRigidbody;

    void Start()
    {
        npcRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                HandleIdle();
                break;
            case State.Patrol:
                HandlePatrol();
                break;
            case State.Chase:
                HandleChase();
                break;
            case State.Attack:
                HandleAttack();
                break;
            case State.Return:
                HandleReturn();
                break;
        }
    }

    void HandleIdle()
    {
        if (PlayerDetected())
            currentState = State.Chase;
    }

    void HandlePatrol()
    {
        PatrolPath();

        if (PlayerDetected())
            currentState = State.Chase;
    }

    void HandleChase()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRadius)
            currentState = State.Attack;
        else
            MoveTowards(playerTransform.position);
    }

    void HandleAttack()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > attackRadius)
            currentState = State.Chase;
    }

    void HandleReturn()
    {
        if (AtStartPosition())
            currentState = State.Idle;
    }

    bool PlayerDetected()
    {
        return Vector3.Distance(transform.position, playerTransform.position) < detectionRadius;
    }

    void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        npcRigidbody.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
    }

    bool AtStartPosition()
    {
        return Vector3.Distance(transform.position, Vector3.zero) < 1.0f; 
    }

    void PatrolPath()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
    }
}
