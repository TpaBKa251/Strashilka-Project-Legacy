
using UnityEngine;


public class EnemyAttackState : EnemyBaseState
{
    float timer;
    public override void Enter(EnemyStatesManager enemy, Enemy _enemy)
    {
        _enemy.player.GetComponent<CharacterController>().enabled = false;
        timer = 2;
    }

    public override void Exit(EnemyStatesManager enemy, Enemy _enemy)
    {
        _enemy.player.GetComponent <CharacterController>().enabled = true;
    }

    public override void Update(EnemyStatesManager enemy, Enemy _enemy)
    {
        timer -= Time.deltaTime;
        if (timer < 0) 
        {
            EnemyStatesManager.isDead = true;
            enemy.deadPanel.SetActive(true);
            Time.timeScale = 0;
            enemy.SwitchState(null);
        }

    }

    
}
