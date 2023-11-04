
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatesManager : MonoBehaviour 
{
    public EnemyBaseState currentState;
    public EnemyBaseState lastState;
    public EnemyBaseState nextState;

    public EnemyCalmState CalmState = new EnemyCalmState();
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyBackState BackState = new EnemyBackState();
    public EnemyHurtState HurtState = new EnemyHurtState();
    public EnemyDeadState DeadState = new EnemyDeadState();
    public EnemyWaitState WaitState = new EnemyWaitState(); 
    public EnemyAttackState AttackState = new EnemyAttackState();
    public EnemyHearState HearState = new EnemyHearState(); 
    public EnemySeeState SeeState = new EnemySeeState();

    public Enemy enemy;
    public float timerToPatrol;

    public GameObject deadPanel;
    public static bool isDead;
    public Vector3 soundPosition;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        timerToPatrol = Random.Range(60, 120);
        currentState = CalmState;
        currentState.Enter(this, enemy);
        deadPanel.SetActive(false);
        isDead = false;
        
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update(this, enemy);
            if (currentState is EnemyCalmState || lastState is EnemyCalmState)
            {
                enemy.SetHealth(100);
                enemy.isTakeDamage = false;
            }
        }
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState.Exit(this, enemy);

        state?.Enter(this, enemy);
        currentState = state;   
    }

    public void HearSomeThing(Vector3 position)
    {
        if ((currentState is EnemyPatrolState || (currentState is EnemyBackState && lastState is EnemyPatrolState)) && enemy.health > 0)
        {
            nextState = HearState;
            soundPosition = position;
            SwitchState(WaitState);
            enemy.SetSpeed(6);
        }
    }

    

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Again()
    {
        SceneManager.LoadScene(1);
    }



}
