using UnityEngine;
using UnityEngine.AI;
using static Constants;

public class EnemyStateChase: EnemyState, ICharacterState
{
    public EnemyStateChase(EnemyController enemyController, Animator animator, NavMeshAgent navMeshAgent) 
        : base(enemyController, animator, navMeshAgent) { }

    public void Enter()
    {
        _animator.SetBool(EnemyAniParamChase, true);
    }

    public void Update()
    {
    }

    public void Exit()
    {
        _animator.SetBool(EnemyAniParamChase, false);
    }
}