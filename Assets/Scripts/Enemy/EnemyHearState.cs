using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHearState : EnemyBaseState
{
    public override void Enter(EnemyStatesManager enemy, Enemy _enemy)
    {
        Debug.Log("I hear something");
        _enemy.agent.SetDestination(enemy.soundPosition);
    }

    public override void Exit(EnemyStatesManager enemy, Enemy _enemy)
    {
        
    }

    public override void Update(EnemyStatesManager enemy, Enemy _enemy)
    {
        if (_enemy.IsInView())
        {
            enemy.nextState = enemy.ChaseState;
            enemy.SwitchState(enemy.WaitState);
        }

        if (_enemy.agent.remainingDistance <= _enemy.agent.stoppingDistance)
        {
            enemy.nextState = enemy.PatrolState;
            enemy.SwitchState(enemy.WaitState);
            Debug.Log("Must be a rat");
        }
    }
}
