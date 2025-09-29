using System;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    public EPlayerState State { get; private set; }

    private Dictionary<EPlayerState, IPlayerState> _states;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        var playerStateIdle = new PlayerStateIdle(this, _animator);

        _states = new Dictionary<EPlayerState, IPlayerState>
        {
            { EPlayerState.Idle, playerStateIdle },
        };
        
        // 상태 초기화
        SetState(EPlayerState.Idle);
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
