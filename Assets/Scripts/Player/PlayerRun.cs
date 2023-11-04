using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : PlayerBaseState
{
    public override void Enter(PlayerStatesManager player, PlayerMovement _player)
    {
        if (Scope.isScoped)
        {
            _player.SetSpeed(4);
        }
        else
        {
            _player.SetSpeed(8);
        }

        Debug.Log("Run");
    }

    public override void Exit(PlayerStatesManager player, PlayerMovement _player)
    {
        player.lastState = this;
    }

    public override void Update(PlayerStatesManager player, PlayerMovement _player)
    {
        player.koefDetectionDistatce = 6f * player.move;

        if (Scope.isScoped)
        {
            _player.SetSpeed(4);
        }
        else
        {
            _player.SetSpeed(8);
        }

        if (!Input.GetButton("RUN"))
        {
            player.SwitchState(player.WalkState);
        }
        if (Input.GetButtonDown("Squat"))
        {
            player.SwitchState(player.SquatState);
        }
        if (Input.GetButtonDown("Jump"))
        {
            player.SwitchState(player.JumpState);
        }
    }
}
