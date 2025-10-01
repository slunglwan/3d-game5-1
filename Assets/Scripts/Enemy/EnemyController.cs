using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Constants;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] private float patrolWaitTime = 1f;
    [SerializeField] private float patrolChance = 30f;
    [SerializeField] private float patrolDetectionDistance = 10f;
    [SerializeField] private LayerMask detactionTargetLayerMask;
    [SerializeField] private float chaseWaitTime = 1f;
    
    // AI 관련
    public float PatrolWaitTime => patrolWaitTime;
    public float PatrolChance => patrolChance;
    public float PatrolDetectionDistance => patrolDetectionDistance;
    public float ChaseWaitTime => chaseWaitTime;
    
    private Collider[] _detectionResults = new Collider[1];
    
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Transform _targetTransform;
    
    // 상태 관리
    public EEnemyState State { get; private set; }
    private Dictionary<EEnemyState, ICharacterState> _states;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        // NavMeshAgent 설정
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = true;
        
        // 플레이어 정보 초기화
        _targetTransform = null;
        
        // 상태 초기화
        var enemyStateIdle = new EnemyStateIdle(this, _animator, _navMeshAgent);
        var enemyStatePatrol = new EnemyStatePatrol(this, _animator, _navMeshAgent);
        var enemyStateChase = new EnemyStateChase(this, _animator, _navMeshAgent);
        var enemyStateAttack = new EnemyStateAttack(this, _animator, _navMeshAgent);

        _states = new Dictionary<EEnemyState, ICharacterState>
        {
            { EEnemyState.Idle, enemyStateIdle },
            { EEnemyState.Patrol, enemyStatePatrol },
            { EEnemyState.Chase, enemyStateChase },
            { EEnemyState.Attack, enemyStateAttack },
        };
        SetState(EEnemyState.Idle);
    }

    private void Update()
    {
        if (State != EEnemyState.None)
        {
            _states[State].Update();
        }
    }

    public void SetState(EEnemyState state)
    {
        if (State == state) return;
        if (State != EEnemyState.None) _states[State].Exit();
        State = state;
        if (State != EEnemyState.None) _states[State].Enter();
    }

    private void OnAnimatorMove()
    {
        var position = _animator.rootPosition;
        _navMeshAgent.nextPosition = position;
        transform.position = position;
    }

    // 일정 거리 안에 Player가 있는지 확인 후 있으면 반환
    // 있을 경우, 이미 찾은 상태면 기존 Player 반환
    // 없으면 null 반환
    public Transform DetectionTargetInCircle()
    {
        if (!_targetTransform)
        {
            Physics.OverlapSphereNonAlloc(transform.position, 
                PatrolDetectionDistance, _detectionResults, detactionTargetLayerMask);
            _targetTransform = _detectionResults[0]?.transform;
        }
        else
        {
            float playerDistance = Vector3.Distance(transform.position, _targetTransform.position);
            if (playerDistance > PatrolDetectionDistance)
            {
                _targetTransform = null;
                _detectionResults[0] = null;
            }
        }
        return _targetTransform;
    }

    private void OnDrawGizmos()
    {
        // 감지 범위
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PatrolDetectionDistance);
        
        // Agent 목적지 표시
        if (_navMeshAgent != null && _navMeshAgent.hasPath)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_navMeshAgent.destination, 0.5f);
            Gizmos.DrawLine(transform.position, _navMeshAgent.destination);
        }
    }
}
