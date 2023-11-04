
using UnityEngine;


public class EnemyPatrolState : EnemyBaseState
{
    float timerToBurrow;
    float timerToSpot;
    public override void Enter(EnemyStatesManager enemy, Enemy _enemy)
    {
        timerToBurrow = Random.Range(20, 30);
        timerToSpot = 2;
    }

    public override void Exit(EnemyStatesManager enemy, Enemy _enemy)
    {
        enemy.lastState = this;

        if (enemy.nextState is EnemySeeState)
        {
            _enemy.agent.SetDestination(_enemy.player.transform.position);
        }
    }

    public override void Update(EnemyStatesManager enemy, Enemy _enemy)
    {
        if (_enemy.health > 0)
        {
            _enemy.Patrol();
            timerToBurrow -= Time.deltaTime;

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

            if (timerToBurrow <= 0 && _enemy.agent.remainingDistance <= _enemy.agent.stoppingDistance)
            {
                enemy.nextState = enemy.BackState;
                enemy.SwitchState(enemy.WaitState);
            }

            if (timerToSpot > 0 && timerToSpot <= 1 && !_enemy.IsInView())
            {
                enemy.nextState = enemy.SeeState;
                enemy.SwitchState(enemy.WaitState);
            }
            
        }
    }
}
