public abstract class PlayerState
{
    protected IPlayer playerC;

    public PlayerState(IPlayer playerC)
    {
        this.playerC = playerC;
    }

    public abstract void ListenToState();
    public abstract void OnStateExit();
}
