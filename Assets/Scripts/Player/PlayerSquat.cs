using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquat : PlayerBaseState
{
    public override void Enter(PlayerStatesManager player, PlayerMovement _player)
    {
        if (Scope.isScoped)
        {
            _player.SetSpeed(1);
        }
        else
        {
            _player.SetSpeed(1.2f);
        }

        PlayerMovement.isSquat = true;

        Debug.Log("Squat");
    }

    public override void Exit(PlayerStatesManager player, PlayerMovement _player)
    {
        
    }

    public override void Update(PlayerStatesManager player, PlayerMovement _player)
    {
        player.koefDetectionDistatce = 1.5f * player.move;

        if (Scope.isScoped)
        {
            _player.SetSpeed(1);
        }
        else
        {
            _player.SetSpeed(1.2f);
        }

        if (Input.GetButtonDown("Squat") && _player.Cum.transform.localPosition.y <= 0.01f)
        {
            player.SwitchState(player.WalkState);
        }

    }
}
