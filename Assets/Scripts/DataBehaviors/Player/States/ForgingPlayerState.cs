using System.Linq;
using Data.Data_Types;
using Data.Interfaces.Player;
using Data.Player;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class ForgingPlayerState : PlayerState
    {
        private readonly PlayerInput input;
        private readonly PlayerBuildData buildData;
        private readonly PlayerStateData stateData;
        private float timeStarted;

        public ForgingPlayerState(PlayerInput input, PlayerBuildData buildData, PlayerStateData stateData) : base(stateData)
        {
            this.input = input;
            this.buildData = buildData;
            this.stateData = stateData;
        }

        private void PlayerInputOnPrimaryKeyReleased()
        {
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        public override void ListenToState()
        {
            if(Time.time - timeStarted > buildData.BuildTime)
            {
                ForgeEssence();
                stateData.ChangeState(PlayerStates.AWAIT_BUILD);
            }
        }

        public override void OnStateExit()
        {
            input.OnPrimaryKeyReleased -= PlayerInputOnPrimaryKeyReleased;
        }

        public override void OnStateEnter()
        {
            input.OnPrimaryKeyReleased += PlayerInputOnPrimaryKeyReleased;
            timeStarted = Time.time;
        }
        
        private void ForgeEssence() //to-do: pool
        {
            if(buildData.CurrentEssence == null) return;
            var essence = GameObject.Instantiate(buildData.CurrentEssence, buildData.TargetAttraction.transform.position, Quaternion.identity);
            buildData.TargetAttraction.AssignEssence(essence);
        }
    }
}