using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;

public class PlayerStateMove: PlayerState, IPlayerState
{
    public PlayerStateMove(PlayerController playerController, Animator animator, PlayerInput playerInput) 
        : base(playerController, animator, playerInput) { }

    public void Enter()
    {
        // Idle 애니메이션 실행
        _animator.SetBool(PlayerAniParamMove, true);
        // Player Input에 대한 액션 할당
        _playerInput.actions["Fire"].performed += Attack;
        _playerInput.actions["Jump"].performed += Jump;
    }

    public void Update()
    {
    }

    public void Exit()
    {
        // Idle 애니메이션 중단
        _animator.SetBool(PlayerAniParamMove, false);
        // Player Input에 대한 액션 할당 해제
        _playerInput.actions["Fire"].performed -= Attack;
        _playerInput.actions["Jump"].performed -= Jump;
    }
}