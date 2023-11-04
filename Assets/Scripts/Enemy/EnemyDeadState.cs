
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeadState : EnemyBaseState
{
    public override void Enter(EnemyStatesManager enemy, Enemy _enemy)
    {
         
    }

    public override void Exit(EnemyStatesManager enemy, Enemy _enemy)
    {
        if (_enemy.health <= 0)
        {
            _enemy.SetHealth(100);
        }

    }

    public override void Update(EnemyStatesManager enemy, Enemy _enemy)
    {
        if (enemy.timerToPatrol <= 0 && enemy.lastState is EnemyCalmState && _enemy.GetComponent<NavMeshAgent>().enabled)
        {
            enemy.nextState = enemy.PatrolState;
            enemy.SwitchState(enemy.WaitState);
        }
        else if (_enemy.GetComponent<NavMeshAgent>().enabled)
        {
            enemy.SwitchState(enemy.lastState);
        }
    }
}
