using System.Linq;
using Data.Data_Types;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class ForgingPlayerState : PlayerState
    {
        private readonly PlayerStateMachine stateMachine;
        private float timeStarted;

        public ForgingPlayerState(PlayerComponent player, PlayerStateMachine stateMachine) : base(player)
        {
            this.stateMachine = stateMachine;
        }

        private void PlayerInputOnPrimaryKeyReleased()
        {
            stateMachine.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        public override void ListenToState()
        {
            if(Time.time - timeStarted > player.BuildData.BuildTime)
            {
                ForgeEssence();
                stateMachine.ChangeState(PlayerStates.AWAIT_BUILD);
            }
        }

        public override void OnStateExit()
        {
            player.PlayerInput.OnPrimaryKeyReleased -= PlayerInputOnPrimaryKeyReleased;
        }

        public override void OnStateEnter()
        {
            player.PlayerInput.OnPrimaryKeyReleased += PlayerInputOnPrimaryKeyReleased;
            timeStarted = Time.time;
        }
        
        private void ForgeEssence() //to-do: pool
        {
            if(player.BuildData.CurrentEssence == null) return;
            var essence = GameObject.Instantiate(player.BuildData.CurrentEssence, player.BuildData.TargetAttraction.transform.position, Quaternion.identity);
            player.BuildData.TargetAttraction.AssignEssence(essence);
        }
    }
}