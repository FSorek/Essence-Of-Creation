public abstract class PlayerState
{
    protected PlayerController playerC;

    public PlayerState(PlayerController playerC)
    {
        this.playerC = playerC;
    }

    public abstract void ListenToState();
    public abstract void OnStateExit();
}
