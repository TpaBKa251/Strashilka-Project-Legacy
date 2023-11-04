
using UnityEngine;


public class EnemyHurtState : EnemyBaseState
{
    float timer;
    Vector3 playerLastPosition;

    public override void Enter(EnemyStatesManager enemy, Enemy _enemy)
    {
        timer = 2;
        _enemy.agent.Stop();
    }

    public override void Exit(EnemyStatesManager enemy, Enemy _enemy)
    {
        _enemy.agent.Resume();
        _enemy.isTakeDamage = false;

        if (_enemy.health > 0)
        {
            _enemy.agent.SetDestination(playerLastPosition);
            
        }
        
    }

    public override void Update(EnemyStatesManager enemy, Enemy _enemy)
    {
        timer -= Time.deltaTime;

        if (_enemy.health <= 0)
        {
            enemy.SwitchState(enemy.BackState);
        }

        if (_enemy.isTakeDamage && _enemy.health > 0)
        {
            timer = 2;
            _enemy.isTakeDamage = false;
            _enemy.agent.Stop();
        }
        
        if (timer <= 0 && _enemy.health > 0) 
        {
            playerLastPosition = _enemy.player.transform.position;
            enemy.SwitchState(enemy.PatrolState);
        }
        
    }
}
