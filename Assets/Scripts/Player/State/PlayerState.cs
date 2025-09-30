
using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;

public class PlayerState
{
    protected PlayerController _playerController;
    protected Animator _animator;
    protected PlayerInput _playerInput;
    
    public PlayerState(PlayerController playerController, Animator animator, PlayerInput playerInput)
    {
        _playerController = playerController;
        _animator = animator;
        _playerInput = playerInput;
    }
    
    protected void Attack(InputAction.CallbackContext context)
    {
        _playerController.SetState(EPlayerState.Attack);
    }
    
    protected void Jump(InputAction.CallbackContext context)
    {
        _playerController.SetState(EPlayerState.Jump);
    }
}