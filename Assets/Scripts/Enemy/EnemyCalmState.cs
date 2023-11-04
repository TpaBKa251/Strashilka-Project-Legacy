
using UnityEngine;

public class EnemyCalmState : EnemyBaseState
{

    float timerToBurrow;

    public override void Enter(EnemyStatesManager enemy, Enemy _enemy)
    {
        timerToBurrow = Random.Range(20, 30);
    }

    public override void Exit(EnemyStatesManager enemy, Enemy _enemy)
    {
        enemy.lastState = this;
    }

    public override void Update(EnemyStatesManager enemy, Enemy _enemy)
    {
        
        timerToBurrow -= Time.deltaTime;

        if (enemy.timerToPatrol > 0)
        {
            enemy.timerToPatrol -= Time.deltaTime;
            _enemy.Patrol();
        }
        else
        {
            enemy.SwitchState(enemy.BackState);
        }
        
        if (timerToBurrow <= 0)
        {
            enemy.SwitchState(enemy.BackState);
        }
    }
}
