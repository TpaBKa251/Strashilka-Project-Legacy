
using UnityEngine;
using UnityEngine.AI;

public class EnemyBackState : EnemyBaseState
{
    float timerToSpot;

    public override void Enter(EnemyStatesManager enemy, Enemy _enemy)
    {
        timerToSpot = 2;

        if ((enemy.lastState is EnemyCalmState || enemy.lastState is EnemyPatrolState) && _enemy.health > 0)
        {
            _enemy.GetComponent<NavMeshAgent>().speed = 3.5f;
        }
        else if (_enemy.health <= 0)
        {
            _enemy.GetComponent<NavMeshAgent>().speed = 15;
        }

        _enemy.Back();
    }

    public override void Exit(EnemyStatesManager enemy, Enemy _enemy)
    {
        
    }

    public override void Update(EnemyStatesManager enemy, Enemy _enemy)
    {
        if (_enemy.agent.remainingDistance <= _enemy.agent.stoppingDistance)
        {
            _enemy.StartCoroutine(_enemy.Appear());
            enemy.SwitchState(enemy.DeadState);
        }

        if (enemy.lastState is EnemyPatrolState && _enemy.health > 0)
        {
            if (_enemy.IsInView() || _enemy.distanceToPlayer <= _enemy.detectionDistance)
            {
                if (_enemy.IsInView())
                {
                    timerToSpot -= (Time.deltaTime * 1 / _enemy.distanceToPlayer * 30);
                    Debug.Log(timerToSpot);
                    if (timerToSpot <= 0)
                    {
                        enemy.nextState = enemy.ChaseState;
                        enemy.SwitchState(enemy.WaitState);
                    }
                }
                else if (_enemy.distanceToPlayer <= _enemy.detectionDistance)
                {
                    enemy.nextState = enemy.ChaseState;
                    enemy.SwitchState(enemy.WaitState);
                }
            }
            else if (_enemy.isTakeDamage)
            {
                enemy.SwitchState(enemy.HurtState);
            }

            if (timerToSpot > 0 && timerToSpot <= 1 && !_enemy.IsInView())
            {
                enemy.nextState = enemy.SeeState;
                enemy.SwitchState(enemy.WaitState);
            }
        }
    }
}
