using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatesManager : MonoBehaviour
{
    public PlayerBaseState currentState;
    public PlayerBaseState lastState;
    public PlayerBaseState nextState;

    public PlayerWalk WalkState = new PlayerWalk();
    public PlayerRun RunState = new PlayerRun();
    public PlayerSquat SquatState = new PlayerSquat();
    public PlayerJump JumpState = new PlayerJump();
    public PlayerDrag DragState = new PlayerDrag();

    PlayerMovement player;
    public Enemy enemy;

    public float koefDetectionDistatce;
    public float move;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
        currentState = WalkState;
        currentState.Enter(this, player);
    }

    private void Update()
    {
        move = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")); 
        if (move > 1) //позволяет не увеличивать скорость ходьбы по диагонали
        {
            move = 1;
        }

        enemy.SetDetectionDistance(koefDetectionDistatce);
        currentState.Update(this, player);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState.Exit(this, player);

        state?.Enter(this, player); //если state не пустой, то вызывается метод Enter()
        currentState = state;
    }

    public PlayerBaseState GetCurrentState()
    {
        return currentState;
    }

    public void Drag()
    {
        if (currentState is not PlayerDrag)
        {
            SwitchState(DragState);
        }
        else
        {
            SwitchState(WalkState);
        }
        
    }
}
