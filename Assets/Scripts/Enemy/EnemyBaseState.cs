public abstract class EnemyBaseState
{
    public abstract void Enter(EnemyStatesManager enemy, Enemy _enemy);
    public abstract void Exit(EnemyStatesManager enemy, Enemy _enemy);
    public abstract void Update(EnemyStatesManager enemy, Enemy _enemy);
}
