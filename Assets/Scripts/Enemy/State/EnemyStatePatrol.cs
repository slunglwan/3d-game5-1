using UnityEngine;
using UnityEngine.AI;
using static Constants;

public class EnemyStatePatrol: EnemyState, ICharacterState
{
    public EnemyStatePatrol(EnemyController enemyController, Animator animator, NavMeshAgent navMeshAgent) 
        : base(enemyController, animator, navMeshAgent) { }

    public void Enter()
    {
        _animator.SetBool(EnemyAniParamPatrol, true);
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        _animator.SetBool(EnemyAniParamPatrol, false);
    }
}