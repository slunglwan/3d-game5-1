

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateIdle: PlayerState, IPlayerState
{
    public PlayerStateIdle(PlayerController playerController, Animator animator, PlayerInput playerInput) 
        : base(playerController, animator, playerInput) { }

    public void Enter()
    {
        _animator.SetBool("idle", true);
        
        // Player Input에 대한 액션 할당
        _playerInput.actions["Fire"].performed += Attack;
        _playerInput.actions["Jump"].performed += Jump;
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}