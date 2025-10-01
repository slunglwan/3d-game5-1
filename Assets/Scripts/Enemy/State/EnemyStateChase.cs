using UnityEngine;
using UnityEngine.AI;
using static Constants;

public class EnemyStateChase: EnemyState, ICharacterState
{
    public EnemyStateChase(EnemyController enemyController, Animator animator, NavMeshAgent navMeshAgent) 
        : base(enemyController, animator, navMeshAgent) { }

    public void Enter()
    {
        _navMeshAgent.isStopped = false;
        _animator.SetBool(EnemyAniParamChase, true);
    }

    public void Update()
    {
        var detectionTargetTransform = _enemyController.DetectionTargetInCircle();
        if (detectionTargetTransform)
        {
            _navMeshAgent.SetDestination(detectionTargetTransform.position);
        }
        else
        {
            _enemyController.SetState(EEnemyState.Idle);
        }
    }

    public void Exit()
    {
        _animator.SetBool(EnemyAniParamChase, false);
    }
}