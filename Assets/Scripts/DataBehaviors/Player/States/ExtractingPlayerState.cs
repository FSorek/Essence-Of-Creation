using Data.Data_Types.Enums;
using Data.Interfaces.StateMachines;
using Data.ScriptableObjects.Player;
using Monobehaviors.Essences.Attacks;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class ExtractingPlayerState : IState
    {
        private readonly PlayerInput playerInput;
        private readonly PlayerBuildData buildData;
        private readonly PlayerStateData stateData;
        private float timeStarted;

        public ExtractingPlayerState(PlayerInput playerInput, PlayerBuildData buildData, PlayerStateData stateData)
        {
            this.playerInput = playerInput;
            this.buildData = buildData;
            this.stateData = stateData;
        }

        public void StateEnter()
        {
            playerInput.OnSecondaryKeyReleased += PlayerInputOnSecondaryKeyReleased;
            timeStarted = Time.time;
        }


        public void ListenToState()
        {
            if(Time.time - timeStarted > buildData.BuildTime)
            {
                ExtractEssence();
                stateData.ChangeState(PlayerStates.WEAVE_ESSENCE);
            }
        }

        private void ExtractEssence()
        {
            var essence = buildData.TargetAttraction.CurrentEssence;
            buildData.ExtractedEssences.Add(essence);
            buildData.TargetAttraction.ClearCurrentEssence();
            
            essence.transform.SetParent(buildData.ConstructorObject);
            essence.transform.position = buildData.ConstructorObject.position + Vector3.down * 3f;
            var attacks = essence.GetComponents<Attack>();

            for (int i = 0; i < attacks.Length; i++)
            {
                attacks[i].enabled = false;
            }
        }

        public void StateExit()
        {
            playerInput.OnSecondaryKeyReleased -= PlayerInputOnSecondaryKeyReleased;
        }
        private void PlayerInputOnSecondaryKeyReleased()
        {
            if(buildData.ExtractedEssences.Count > 0)
                stateData.ChangeState(PlayerStates.WEAVE_ESSENCE);
            else
                stateData.ChangeState(PlayerStates.AWAIT_BUILD);
        }
    }
}