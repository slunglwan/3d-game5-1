using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform headTransform;
    
    // 컴포넌트 캐싱
    private Animator _animator;
    private PlayerInput _playerInput;
    
    // 상태 정보
    public EPlayerState State { get; private set; }
    private Dictionary<EPlayerState, IPlayerState> _states;

    private void Awake()
    {
        // 컴포넌트 초기화
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        
        // 상태 객체 초기화
        var playerStateIdle = new PlayerStateIdle(this, _animator, _playerInput);
        _states = new Dictionary<EPlayerState, IPlayerState>
        {
            { EPlayerState.Idle, playerStateIdle },
        };
        // 상태 초기화
        SetState(EPlayerState.Idle);
    }

    private void OnEnable()
    {
        // 카메라 초기화
        _playerInput.camera = Camera.main;
        if (_playerInput.camera != null)
        {
            _playerInput.camera.GetComponent<CameraController>().SetTarget(headTransform, _playerInput);
        }
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        if (State != EPlayerState.None)
        {
            _states[State].Update();
        }
    }

    // 새로운 상태를 할당하는 함수
    public void SetState(EPlayerState state)
    {
        if (State == state) return;
        if (State != EPlayerState.None) _states[State].Exit();
        State = state;
        if (State != EPlayerState.None) _states[State].Enter();
    }
}
