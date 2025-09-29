

using UnityEngine;

public class PlayerStateIdle: PlayerState, IPlayerState
{
    public PlayerStateIdle(PlayerController playerController, Animator animator) 
        : base(playerController, animator) { }
    
    // 상태 진입시 할 일
    public void Enter()
    {
        
    }

    // 상태 중일 때 할 일
    public void Update()
    {
        
    }

    // 상태 해지 시 할 일
    public void Exit()
    {
        
    }
}