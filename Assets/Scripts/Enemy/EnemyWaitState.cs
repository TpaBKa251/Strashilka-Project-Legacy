using UnityEngine;

public class EnemyWaitState : EnemyBaseState
{
    float timer;

    public override void Enter(EnemyStatesManager enemy, Enemy _enemy)
    {
        _enemy.agent.Stop();
        if (enemy.nextState is EnemyPatrolState)
        {
            timer = 5;
        }

        if (enemy.nextState is EnemyChaseState)
        {
            timer = 1;
        }

        if (enemy.nextState is EnemyBackState)
        {
            timer = 5;
        }

        if (enemy.nextState is EnemyHearState || enemy.nextState is EnemySeeState)
        {
            timer = 2;
        }
        
    }

    public override void Exit(EnemyStatesManager enemy, Enemy _enemy)
    {
        _enemy.agent.Resume();
    }

    public override void Update(EnemyStatesManager enemy, Enemy _enemy)
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            enemy.SwitchState(enemy.nextState);
        }

    }
}
