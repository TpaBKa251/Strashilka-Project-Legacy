using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerWalk : PlayerBaseState
{
    public override void Enter(PlayerStatesManager player, PlayerMovement _player)
    {
        if (Scope.isScoped)
        {
            _player.SetSpeed(2);
        }
        else
        {
            _player.SetSpeed(4);
        }

        PlayerMovement.isSquat = false;

        Debug.Log("Walk");
    }

    public override void Exit(PlayerStatesManager player, PlayerMovement _player)
    {
        player.lastState = this;
    }
    
    public override void Update(PlayerStatesManager player, PlayerMovement _player)
    {
        player.koefDetectionDistatce = 3f * player.move;

        if (Scope.isScoped)
        {
            _player.SetSpeed(2);
        }
        else
        {
            _player.SetSpeed(4);
        }

        if (Input.GetButtonDown("RUN"))
        {
            player.SwitchState(player.RunState);
        }

        if (Input.GetButtonDown("Squat") && _player.Cum.transform.localPosition.y >= .79f)
        {
            player.SwitchState(player.SquatState);
        }

        if (Input.GetButtonDown("Jump"))
        {
            player.SwitchState(player.JumpState);
        }
    }
}
