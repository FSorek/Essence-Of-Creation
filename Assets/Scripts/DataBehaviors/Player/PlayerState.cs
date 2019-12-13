using Data.Data_Types;
using Data.Interfaces.Player;
using Monobehaviors.Player;

namespace DataBehaviors.Player
{
    public abstract class PlayerState
    {
        protected PlayerStateData stateData;

        public PlayerState(PlayerStateData stateData)
        {
            this.stateData = stateData;
        }

        public abstract void ListenToState();
        public abstract void OnStateExit();
        public abstract void OnStateEnter();
    }
}