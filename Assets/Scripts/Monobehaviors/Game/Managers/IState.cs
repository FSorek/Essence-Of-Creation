namespace Monobehaviors.Game.Managers
{
    public interface IState
    {
        void StateEnter();
        void ListenToState();
        void StateExit();
    }
}