public abstract class PlayerBaseState
{
    public abstract void Enter(PlayerStatesManager player, PlayerMovement _player);
    public abstract void Exit(PlayerStatesManager player, PlayerMovement _player);
    public abstract void Update(PlayerStatesManager player, PlayerMovement _player);
}
