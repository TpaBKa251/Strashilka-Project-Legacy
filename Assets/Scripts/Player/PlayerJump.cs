using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerJump : PlayerBaseState
{
    float timer;

    public override void Enter(PlayerStatesManager player, PlayerMovement _player)
    {
        player.koefDetectionDistatce = _player.speed * 1.5f;

        timer = .1f;

        if (_player.isGrounded)
        {
            _player.velocity.y = Mathf.Sqrt(_player.jumpHeight * -2f * _player.gravity);
        }

        Debug.Log("Jump");
        
    }

    public override void Exit(PlayerStatesManager player, PlayerMovement _player)
    {
        
    }

    public override void Update(PlayerStatesManager player, PlayerMovement _player)
    {
        timer -= Time.deltaTime;

        if (_player.isGrounded && timer <= 0)
        {
            if (Input.GetButton("RUN"))
            {
                player.SwitchState(player.RunState);
            }
            else
            {
                player.SwitchState(player.lastState);
            }
            
        }
    }
}
