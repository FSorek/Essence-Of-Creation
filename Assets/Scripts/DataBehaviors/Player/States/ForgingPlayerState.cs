using System.Linq;
using Data.Data_Types;
using Data.Interfaces.Player;
using Data.Player;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.BuildSpot;
using Monobehaviors.Game.Managers;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class ForgingPlayerState : IState
    {
        private readonly PlayerStateData stateData;
        private readonly PlayerInput input;
        private readonly PlayerBuildData buildData;
        private float timeStarted;

        public ForgingPlayerState(PlayerInput input, PlayerBuildData buildData, PlayerStateData stateData)
        {
            this.input = input;
            this.buildData = buildData;
            this.stateData = stateData;
        }

        private void PlayerInputOnPrimaryKeyReleased()
        {
            stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }

        public void ListenToState()
        {
            if(Time.time - timeStarted > buildData.BuildTime)
            {
                ForgeEssence();
                stateData.ChangeState(PlayerStates.AWAIT_BUILD);
            }
        }

        public void StateExit()
        {
            input.OnPrimaryKeyReleased -= PlayerInputOnPrimaryKeyReleased;
        }

        public void StateEnter()
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