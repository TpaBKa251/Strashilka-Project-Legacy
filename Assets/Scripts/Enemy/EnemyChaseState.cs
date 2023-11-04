
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    float timer = 3;
    Vector3 playerLastPosition;
    bool toPatrol;

    public override void Enter(EnemyStatesManager enemy, Enemy _enemy)
    {
        timer = 3;
    }

    public override void Exit(EnemyStatesManager enemy, Enemy _enemy)
    {
        
    }

    public override void Update(EnemyStatesManager enemy, Enemy _enemy)
    {
        if (_enemy.health > 0)
        {
            timer -= Time.deltaTime;

            if (_enemy.IsInView() || _enemy.distanceToPlayer <= _enemy.detectionDistance)
            {
                timer = 3;
            }
            if (_enemy.isTakeDamage)
            {
                enemy.SwitchState(enemy.HurtState);
            }

            if (timer > 0)
            {
                _enemy.Follow();
            }
            else if (timer <= 0 && !toPatrol)
            {
                toPatrol = true;
                playerLastPosition = _enemy.player.transform.position;
                _enemy.agent.SetDestination(playerLastPosition);
            }

            if (toPatrol)
            {
                if (_enemy.agent.remainingDistance <= _enemy.agent.stoppingDistance && !_enemy.IsInView() && _enemy.health > 0)
                {
                    toPatrol = false;
                    enemy.nextState = enemy.PatrolState;
                    enemy.SwitchState(enemy.WaitState);
                }
                else if (_enemy.IsInView())
                {
                    timer = 3;
                    toPatrol = false;
                }
            }

            if (_enemy.distanceToPlayer <= _enemy.agent.stoppingDistance)
            {
                enemy.SwitchState(enemy.AttackState);
            }
        }
        
        
    }
}
