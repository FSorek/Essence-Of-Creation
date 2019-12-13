using Data.Data_Types;
using Data.Interfaces.Player;
using Monobehaviors.Player;

namespace DataBehaviors.Player
{
    public abstract class PlayerState
    {
        protected PlayerComponent player;

        public PlayerState(PlayerComponent player)
        {
            this.player = player;
        }

        public abstract PlayerStates ListenToState();
        public abstract void OnStateExit();
        public abstract void OnStateEnter();
    }
}