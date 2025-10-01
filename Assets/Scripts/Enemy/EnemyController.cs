using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Constants;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    
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
    }

    public void SetState(EEnemyState state)
    {
        if (State == state) return;
        if (State != EEnemyState.None) _states[State].Exit();
        State = state;
        if (State != EEnemyState.None) _states[State].Enter();
    }
}
