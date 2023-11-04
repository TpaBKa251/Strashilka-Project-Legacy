using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrag : PlayerBaseState
{
    public override void Enter(PlayerStatesManager player, PlayerMovement _player)
    {
        _player.SetSpeed(1);

        PlayerMovement.isSquat = false;

        Debug.Log("Damn, its heavy");
    }

    public override void Exit(PlayerStatesManager player, PlayerMovement _player)
    {
        
    }

    public override void Update(PlayerStatesManager player, PlayerMovement _player)
    {

    }
}
