using Data.Interfaces.Player;
using Monobehaviors.Player;

namespace DataBehaviors.Player
{
    public abstract class PlayerState
    {
        protected PlayerComponent playerComponentC;

        public PlayerState(PlayerComponent playerComponentC)
        {
            this.playerComponentC = playerComponentC;
        }

        public abstract void ListenToState();
        public abstract void OnStateExit();
    }
}