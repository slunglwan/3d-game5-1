
using UnityEngine;
using UnityEngine.InputSystem;

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
}