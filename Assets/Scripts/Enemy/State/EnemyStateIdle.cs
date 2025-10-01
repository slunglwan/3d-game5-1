using UnityEngine;
using UnityEngine.AI;
using static Constants;

public class EnemyStateIdle: EnemyState, ICharacterState
{
    public EnemyStateIdle(EnemyController enemyController, Animator animator, NavMeshAgent navMeshAgent) 
        : base(enemyController, animator, navMeshAgent) { }

    public void Enter()
    {
        _animator.SetBool(EnemyAniParamIdle, true);
    }

    public void Update()
    {
    }

    public void Exit()
    {
        _animator.SetBool(EnemyAniParamIdle, false);
    }
}